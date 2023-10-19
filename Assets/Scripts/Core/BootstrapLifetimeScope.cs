using VContainer;
using VContainer.Unity;

public class BootstrapLifetimeScope : LifetimeScope
{
    public GameSettings settings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(settings).AsSelf();

        builder.RegisterEntryPoint<GameManager>(Lifetime.Singleton).AsSelf();


    }

}
