﻿using ScreenSystem;
using System;
using System.Collections;
using UI.Screens;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

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
        _scope.StartCoroutine(LoadSceneAsync());
    }

    public void LevelEnded()
    {
        ScreensManager.HideScreen<GameScreen>();
        ScreensManager.ShowScreen<LevelEndScreen>().SetCallback(RestartGame);
    }

    private void RestartGame()
    {
        ScreensManager.HideScreen<LevelEndScreen>();
        _scope.StartCoroutine(ReloadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        ScreenSystem.ScreensManager.ShowScreen<LoadingScreen>();

        // LifetimeScope generated in this block will be parented by `this.lifetimeScope`
        using (LifetimeScope.EnqueueParent(_scope))
        {
            // If this scene has a LifetimeScope, its parent will be `parent`.
            var loading = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            while (!loading.isDone)
            {
                yield return null;
            }
        }

        ScreenSystem.ScreensManager.HideScreen<LoadingScreen>();

        ScreensManager.ShowScreen<StartScreen>().SetCallback(StartGame);
    }

    private IEnumerator ReloadSceneAsync()
    {
        ScreenSystem.ScreensManager.ShowScreen<LoadingScreen>();

        var loading = SceneManager.UnloadSceneAsync("Game");
        while (!loading.isDone)
        {
            yield return null;
        }

        yield return LoadSceneAsync();
    }

    private void StartGame()
    {
        ScreensManager.HideScreen<StartScreen>();

        ScreensManager.ShowScreen<GameScreen>().SetController();

        GameStarted?.Invoke();
    }

}
