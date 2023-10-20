using UnityEngine;
using VContainer.Unity;

public class SpawnPointComponent : MonoBehaviour
{
    [SerializeField]
    private EnemyComponent enemyPrefab;
    public EnemyComponent SpawnEnemy => enemyPrefab;
    public EnemyComponent SpawnedEnemy { get; set; }
}
