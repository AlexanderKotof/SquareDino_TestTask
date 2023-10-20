using TestTask.Components;
using UnityEngine;
using VContainer.Unity;

namespace TestTask.Context.Components
{
    public class SpawnPointComponent : MonoBehaviour
    {
        [SerializeField]
        private EnemyComponent enemyPrefab;
        public EnemyComponent SpawnEnemy => enemyPrefab;
        public EnemyComponent SpawnedEnemy { get; set; }
    }
}