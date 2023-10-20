using TestTask.Core;
using TestTask.Input;
using VContainer;
using VContainer.Unity;

namespace TestTask.Scopes
{
    public class BootstrapLifetimeScope : LifetimeScope
    {
        public GameSettings settings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(settings).AsSelf();

            builder.RegisterEntryPoint<GameManager>().AsSelf();
        }
    }
}