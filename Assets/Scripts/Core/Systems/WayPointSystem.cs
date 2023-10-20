using System;
using TestTask.Context;
using TestTask.Context.Components;
using TestTask.Core;
using VContainer.Unity;

namespace TestTask.GameSystems
{
    public class WayPointSystem : IInitializable, IDisposable
    {
        private readonly GameManager gameManager;
        private readonly WayPoints context;
        private readonly PlayerSpawnSystem playerSystem;
        private readonly PlayerMovementSystem movement;
        private readonly ShootingSystem shooting;

        private int _currentWayPointIndex;

        private WayPointComponent CurrentWaypoint => context.points.Length <= _currentWayPointIndex ? null : context.points[_currentWayPointIndex];

        public WayPointSystem(GameManager gameManager, WayPoints context, PlayerSpawnSystem playerSystem, PlayerMovementSystem movement, ShootingSystem shooting)
        {
            this.gameManager = gameManager;
            this.context = context;
            this.playerSystem = playerSystem;
            this.movement = movement;
            this.shooting = shooting;
        }
        public void Initialize()
        {
            gameManager.GameStarted += OnGameStarted;
            shooting.OnEnemyDied += OnEnemyDied;
        }
        public void Dispose()
        {
            shooting.OnEnemyDied -= OnEnemyDied;
            gameManager.GameStarted -= OnGameStarted;
        }
        private void OnGameStarted()
        {
            _currentWayPointIndex = 0;

            MoveToNextWaypoint();
        }

        private void MoveToNextWaypoint()
        {
            _currentWayPointIndex++;

            if (CurrentWaypoint == null)
            {
                gameManager.LevelEnded();
                return;
            }

            movement.MoveToWaypoint(CurrentWaypoint, OnWayPointReached);
            shooting.SetActive(false);
        }

        private void OnWayPointReached(WayPointComponent wayPoint)
        {
            if (wayPoint.wayPointEnemySpawns.Length > 0)
            {
                ActivateShooting(wayPoint);
                return;
            }

            MoveToNextWaypoint();
        }

        private void ActivateShooting(WayPointComponent wayPoint)
        {
            shooting.SetActive(true);

            foreach (var enemy in wayPoint.wayPointEnemySpawns)
            {
                enemy.SpawnedEnemy.ShowHealthbar(playerSystem.Player);
            }
        }

        private void OnEnemyDied()
        {
            bool wayPointCleared = true;
            foreach (var enemy in CurrentWaypoint.wayPointEnemySpawns)
            {
                if (!enemy.SpawnedEnemy.IsDied)
                {
                    wayPointCleared = false;
                    break;
                }
            }

            if (!wayPointCleared)
                return;

            MoveToNextWaypoint();
        }
    }
}