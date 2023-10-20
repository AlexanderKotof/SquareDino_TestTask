using ScreenSystem;
using System;
using System.Collections;
using TestTask.UI.Screens;
using UI.Screens;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace TestTask.Core
{
    public class GameManager : IStartable
    {
        private const string _gameSceneName = "Game";

        private LifetimeScope _scope;

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
            // LifetimeScope generated in this block will be parented by `this.lifetimeScope`
            using (LifetimeScope.EnqueueParent(_scope))
            {
                // If this scene has a LifetimeScope, its parent will be `parent`.
                var loading = SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Additive);
                while (!loading.isDone)
                {
                    yield return null;
                }
            }

            ScreensManager.ShowScreen<StartScreen>().SetCallback(StartGame);
        }

        private IEnumerator ReloadSceneAsync()
        {
            var loading = SceneManager.UnloadSceneAsync(_gameSceneName);
            while (!loading.isDone)
            {
                yield return null;
            }

            yield return LoadSceneAsync();
        }

        private void StartGame()
        {
            ScreensManager.HideScreen<StartScreen>();
            ScreensManager.ShowScreen<GameScreen>();
            GameStarted?.Invoke();
        }

    }
}