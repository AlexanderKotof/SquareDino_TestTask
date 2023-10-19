using ScreenSystem;
using System.Collections;
using UI.Screens;
using UnityEngine;
using VContainer.Unity;

public class PlayerSystem : IInitializable
{
    public PlayerComponent playerPrefab;
    public BulletComponent bulletPrefab;
    public PlayerComponent Player { get; private set; }

    private ShootingSystem _shootingSystem;
    private PlayerMovementSystem _playerMovementSystem;
    private WayPoints _wayPoints;

    private int _currentWayPointIndex = 0;
    private readonly ShootController _shootController;

    public PlayerSystem(GameSettings settings, GameManager gameManager, ShootController shootController)
    {
        playerPrefab = settings.PlayerPrefab;
        bulletPrefab = settings.BulletPrefab;

        gameManager.GameStarted += OnGameStarted;
        _shootController = shootController;
    }

    private void OnGameStarted()
    {
        _currentWayPointIndex = 0;
        ScreensManager.ShowScreen<GameScreen>().SetController(_shootController);
        MoveToNext();
    }

    public void Initialize()
    {
        InstatiatePlayer();
    }

    public void Tick()
    {
        
    }

    private void InstatiatePlayer()
    {
        var firstWayPointTransform = _wayPoints.points[0].transform;
        Player = GameObject.Instantiate(playerPrefab, firstWayPointTransform.position, firstWayPointTransform.rotation);
    }



    private void MoveToNext()
    {
        _currentWayPointIndex++;

        if (_wayPoints.points.Length <= _currentWayPointIndex)
        {
            LevelEnded();
            return;
        }

        var point = _wayPoints.points[_currentWayPointIndex];
        _playerMovementSystem.MoveToWaypoint(point);
    }

    private void LevelEnded()
    {
        _shootingSystem.Dispose();
        _playerMovementSystem.Dispose();

        ScreensManager.HideScreen<GameScreen>();
        ScreensManager.ShowScreen<LevelEndScreen>().SetCallback(RestartGame);
    }

    private void RestartGame()
    {
        ScreensManager.HideScreen<LevelEndScreen>();
        //GameSceneLoader.LoadGameScene(GameSceneLoaded);
    }

    private IEnumerator WaitForEnemiesDie(WayPointComponent wayPoint)
    {
        foreach (var enemy in wayPoint.wayPointEnemySpawns)
        {
            enemy.SpawnedEnemy.Initialize(Player);
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

        MoveToNext();
    }

    private void OnWayPointReached(WayPointComponent wayPoint)
    {
        if (wayPoint.wayPointEnemySpawns.Length > 0)
        {
            //StartCoroutine(WaitForEnemiesDie(wayPoint));
            return;
        }

        MoveToNext();
    }


}
