using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    public SceneContext sceneContext;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(sceneContext);

        builder.RegisterEntryPoint<PlayerSystem>().AsSelf();
        builder.RegisterEntryPoint<ShootingSystem>().AsSelf();
        builder.RegisterEntryPoint<PlayerMovementSystem>().AsSelf();

        builder.RegisterEntryPoint<ShootController>().AsSelf();
    }
}

public class PlayerInputService
{
    public static event Action<Vector3> ShootInput;

    public static void SetShootInput(Vector3 point)
    {
        ShootInput?.Invoke(point);
    }
}
