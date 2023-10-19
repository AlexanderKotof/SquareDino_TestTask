using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{

    public SceneContext sceneContext;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance<SceneContext>(sceneContext);

        builder.RegisterEntryPoint<PlayerSystem>().AsSelf();
        builder.RegisterEntryPoint<ShootingSystem>().AsSelf();
        builder.RegisterEntryPoint<PlayerMovementSystem>().AsSelf();

        builder.RegisterEntryPoint<ShootController>().AsSelf();
    }
}
