using CodeBase.AssetManagement;
using CodeBase.Factory;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Services.Input;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, IInitializable, ICoroutineRunner
    {
        public LoadingCurtain curtain;
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            BindAssetProvider();
            BindGameFactory();
            BindLoadingCurtain(); 
            BindGameStateMachineAndStates();
            BindSceneLoader();
            Container.Bind<IInputService>().To<StandAloneInput>().AsSingle().NonLazy();
        }

        public void Initialize()
        {
           
        }

        private void BindAssetProvider() => Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

        private void BindGameFactory() => 
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

        private void BindLoadingCurtain() => 
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(curtain).AsSingle();


        private void BindSceneLoader() => 
            Container.Bind<SceneLoader>().AsSingle().NonLazy();

        private void BindGameStateMachineAndStates()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StateMachineInitializer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsTransient();
            Container.BindInterfacesAndSelfTo<LoadLevelState>().AsTransient();
            Container.BindInterfacesAndSelfTo<GameLoopState>().AsTransient();
        }
        
    }
}