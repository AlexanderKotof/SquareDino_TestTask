using System;
using System.Collections;
using VContainer.Unity;

public class PlayerMovementSystem : IInitializable, IDisposable
{
    private PlayerComponent _player;
    private int _currentWayPointIndex = 0;

    private const float _distanceThreashold = 0.1f;

    private readonly GameManager manager;
    private readonly SceneContext context;

    public PlayerMovementSystem(PlayerSystem playerSystem, GameManager manager, SceneContext context)
    {
        _player = playerSystem.Player;
        this.manager = manager;
        this.context = context;
    }

    public void Initialize()
    {
        manager.GameStarted += OnGameStarted;
    }

    public void Dispose()
    {
        manager.GameStarted -= OnGameStarted;
        _player = null;
    }

    private void OnGameStarted()
    {
        _currentWayPointIndex = 0;

        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        _currentWayPointIndex++;

        if (context.wayPoints.points.Length <= _currentWayPointIndex)
        {
            manager.LevelEnded();
            return;
        }

        var point = context.wayPoints.points[_currentWayPointIndex];
        MoveToWaypoint(point);
    }

    private void MoveToWaypoint(WayPointComponent wayPointComponent)
    {
        _player.StartCoroutine(MoveToWayPoint(wayPointComponent));
    }

    private IEnumerator MoveToWayPoint(WayPointComponent wayPoint)
    {
        _player.MoveToPosition(wayPoint.transform.position);

        while ((_player.transform.position - wayPoint.transform.position).sqrMagnitude > _distanceThreashold * _distanceThreashold)
        {
            yield return null;
        }

        OnWayPointReached(wayPoint);
    }
    private IEnumerator WaitForEnemiesDie(WayPointComponent wayPoint)
    {
        foreach (var enemy in wayPoint.wayPointEnemySpawns)
        {
            enemy.SpawnedEnemy.Initialize(_player);
        }

        while (true)
        {
            bool wayPointCleared = true;
            foreach (var enemy in wayPoint.wayPointEnemySpawns)
            {
                if (!enemy.SpawnedEnemy.IsDied)
                {
                    wayPointCleared = false;
                    break;
                }
            }

            if (wayPointCleared)
                break;

            yield return null;
        }

        MoveToNextWaypoint();
    }

    private void OnWayPointReached(WayPointComponent wayPoint)
    {
        if (wayPoint.wayPointEnemySpawns.Length > 0)
        {
            _player.StartCoroutine(WaitForEnemiesDie(wayPoint));
            return;
        }

        MoveToNextWaypoint();
    }

}
