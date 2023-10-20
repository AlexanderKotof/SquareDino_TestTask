using UnityEngine;
using VContainer.Unity;

public class PlayerSpawnSystem : IStartable
{
    public PlayerComponent playerPrefab;
    public PlayerComponent Player { get; private set; }


    public PlayerSpawnSystem(GameSettings settings, WayPoints wayPoints)
    {
        playerPrefab = settings.PlayerPrefab;

        InstatiatePlayer(wayPoints.points[0].transform);
    }

    public void Start()
    {
        
    }

    private void InstatiatePlayer(Transform firstWaypoint)
    {
        Player = GameObject.Instantiate(playerPrefab, firstWaypoint.position, firstWaypoint.rotation);
    }
}
