using TestTask.Context;
using TestTask.GameSystems;
using VContainer;
using VContainer.Unity;

namespace TestTask.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        public SceneContext sceneContext;

        protected override void Configure(IContainerBuilder builder)
        {
            SetupScene(builder);

            SetupPlayer(builder);

            SetupEnemies(builder);
        }

        private void SetupScene(IContainerBuilder builder)
        {
            builder.RegisterInstance(sceneContext);
            builder.RegisterInstance(sceneContext.wayPoints);
        }

        private static void SetupPlayer(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayerSpawnSystem>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<ShootingSystem>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<PlayerMovementSystem>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<WayPointSystem>(Lifetime.Scoped).AsSelf();
        }

        private static void SetupEnemies(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EnemySpawnSystem>(Lifetime.Scoped).AsSelf();
        }
    }
}