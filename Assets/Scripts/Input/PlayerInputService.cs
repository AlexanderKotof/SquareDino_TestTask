using System;
using UnityEngine;

public class PlayerInputService
{
    public static event Action<Vector3> ShootInput;

    public static void SetShootInput(Vector3 point)
    {
        ShootInput?.Invoke(point);
    }
}
