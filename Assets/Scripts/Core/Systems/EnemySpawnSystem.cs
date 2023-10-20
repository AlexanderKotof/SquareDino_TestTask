using System;
using System.Collections.Generic;
using TestTask.Components;
using TestTask.Context;
using TestTask.Context.Components;
using UnityEngine;
using VContainer.Unity;

namespace TestTask.GameSystems
{
    public class EnemySpawnSystem : IInitializable
    {
        private readonly SceneContext sceneContext;

        private EnemyComponent[] _enemies;

        //private Dictionary<WayPointComponent, EnemyComponent[]> _waypoints

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
                _enemies[i] = spawnPoint.SpawnedEnemy = UnityEngine.Object.Instantiate(spawnPoint.SpawnEnemy, spawnPoint.transform);
            }
        }
    }
}