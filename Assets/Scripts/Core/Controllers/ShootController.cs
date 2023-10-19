using ScreenSystem;
using System;
using UnityEngine;
using VContainer.Unity;

public class ShootController: IDisposable, IInitializable
{
    private readonly PlayerComponent player;
    private readonly ShootingSystem manager;
    private readonly float _maxShootingDistance;

    public ShootController(PlayerSystem player, GameSettings settings, ShootingSystem manager)
    {
        this.player = player.Player;
        this.manager = manager;

        _maxShootingDistance = settings.ShootingDistance;
    }

    public void Shoot(Vector3 position)
    {
        var ray = player.playerCamera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out var hit, _maxShootingDistance))
        {
            var direction = hit.point - player.bulletSpawnPoint.position;

            manager.SpawnBullet(player.bulletSpawnPoint.position, direction);
            player.Shoot(direction);
        }
    }

    public void Dispose()
    {
        PlayerInputService.ShootInput -= Shoot;
    }

    public void Initialize()
    {
        PlayerInputService.ShootInput += Shoot;
    }
}
