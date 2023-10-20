using TestTask.Components;
using TestTask.Context;
using TestTask.Core;
using UnityEngine;
using VContainer.Unity;

namespace TestTask.GameSystems
{
    public class PlayerSpawnSystem 
    {
        public PlayerComponent playerPrefab;
        public PlayerComponent Player { get; private set; }

        public PlayerSpawnSystem(GameSettings settings, WayPoints wayPoints)
        {
            playerPrefab = settings.PlayerPrefab;

            InstatiatePlayer(wayPoints.points[0].transform);
        }

        private void InstatiatePlayer(Transform firstWaypoint)
        {
            Player = Object.Instantiate(playerPrefab, firstWaypoint.position, firstWaypoint.rotation);
        }
    }
}