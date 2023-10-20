using System;
using UnityEngine;

namespace TestTask.Input
{
    public class PlayerInputService : IPlayerInputService, IDisposable
    {
        public static PlayerInputService Instance { get; private set; }

        public PlayerInputService()
        {
            Instance = this;
        }

        public event Action<Vector3> ShootInput;

        public void SetShootInput(Vector3 point)
        {
            ShootInput?.Invoke(point);
        }

        public void Dispose()
        {
            Instance = null;
            ShootInput = null;
        }
    }

    public interface IPlayerInputService
    {
        event Action<Vector3> ShootInput;

        void SetShootInput(Vector3 point);
    }
}