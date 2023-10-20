using System.Collections;
using System.Collections.Generic;
using TestTask.Context.Components;
using UnityEngine;

namespace TestTask.Context
{
    public class SceneContext : MonoBehaviour
    {
        public WayPoints wayPoints;
        public SpawnPointComponent[] spawnPoints;

        private void OnValidate()
        {
            spawnPoints = FindObjectsOfType<SpawnPointComponent>();
        }
    }
}