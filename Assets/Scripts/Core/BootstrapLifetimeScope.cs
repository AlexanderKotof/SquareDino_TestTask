using ScreenSystem;
using System;
using UI.Screens;
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

public class GameManager : IStartable
{
    private LifetimeScope _scope;

    public event Action GameSceneLoaded;
    public event Action GameStarted;

    public GameManager(LifetimeScope scope)
    {
        _scope = scope;
    }

    public void Start()
    {
        GameSceneLoader.LoadGameScene(() => { });
        ScreensManager.ShowScreen<StartScreen>().SetCallback(StartGame);
    }

    private void StartGame()
    {
        ScreensManager.HideScreen<StartScreen>();

        GameStarted?.Invoke();
    }

}
