using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContext : MonoBehaviour
{
    public WayPoints wayPoints;
    public SpawnPointComponent[] spawnPoints;

    private void OnValidate()
    {
        spawnPoints = FindObjectsOfType<SpawnPointComponent>();
    }
}
