using System;
using UnityEngine;
using VContainer.Unity;

public class EnemySpawnSystem : IInitializable, IDisposable
{
    private readonly SceneContext sceneContext;

    private EnemyComponent[] _enemies;

    public EnemySpawnSystem(SceneContext sceneContext)
    {
        this.sceneContext = sceneContext;
    }

    public void Initialize()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        _enemies = new EnemyComponent[sceneContext.spawnPoints.Length];

        for (int i = 0; i < sceneContext.spawnPoints.Length; i++)
        {
            SpawnPointComponent spawnPoint = sceneContext.spawnPoints[i];
            _enemies[i] = spawnPoint.SpawnedEnemy = GameObject.Instantiate(spawnPoint.SpawnEnemy, spawnPoint.transform);
        }
    }

    public void Dispose()
    {

    }
}
