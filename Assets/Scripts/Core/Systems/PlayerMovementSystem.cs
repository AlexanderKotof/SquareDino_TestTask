using System;
using System.Collections;

public class PlayerMovementSystem
{
    private PlayerComponent _player;

    private const float _distanceThreashold = 0.1f;

    public PlayerMovementSystem(PlayerSpawnSystem playerSystem)
    {
        _player = playerSystem.Player;
    }

    public void MoveToWaypoint(WayPointComponent wayPointComponent, Action<WayPointComponent> onWaypointReach)
    {
        _player.StartCoroutine(MoveToWayPoint(wayPointComponent, onWaypointReach));
    }

    private IEnumerator MoveToWayPoint(WayPointComponent wayPoint, Action<WayPointComponent> onWaypointReach)
    {
        _player.MoveToPosition(wayPoint.transform.position);

        while ((_player.transform.position - wayPoint.transform.position).sqrMagnitude > _distanceThreashold * _distanceThreashold)
        {
            yield return null;
        }

        onWaypointReach(wayPoint);
    }
}
