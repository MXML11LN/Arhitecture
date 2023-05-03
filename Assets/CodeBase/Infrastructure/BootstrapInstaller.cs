using CodeBase.Infrastructure.StateMachine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            BindGameStateMachine();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }

        private void BindGameStateMachine() => 
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
    }
}