using ScreenSystem;
using System.Collections;
using UI.Screens;
using UnityEngine;
using VContainer.Unity;

public class PlayerSystem : IStartable
{
    public PlayerComponent playerPrefab;
    public PlayerComponent Player { get; private set; }


    public PlayerSystem(GameSettings settings, GameManager gameManager, SceneContext context)
    {
        playerPrefab = settings.PlayerPrefab;

        InstatiatePlayer(context.wayPoints.points[0].transform);
    }

    public void Start()
    {
        
    }

    private void InstatiatePlayer(Transform firstWaypoint)
    {
        Player = GameObject.Instantiate(playerPrefab, firstWaypoint.position, firstWaypoint.rotation);
    }
}
