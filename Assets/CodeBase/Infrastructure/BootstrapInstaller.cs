using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            BindGameStateMachine();
            BindSceneLoader();
        }

        public void Initialize() => 
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();

        private void BindSceneLoader() => 
            Container.Bind<SceneLoader>().AsSingle().NonLazy();

        private void BindGameStateMachine() => 
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
    }
}