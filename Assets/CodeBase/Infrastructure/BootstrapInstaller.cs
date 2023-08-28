using CodeBase.AssetManagement;
using CodeBase.Factory;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Random;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.StaticData;
using Zenject;
using Application = UnityEngine.Application;

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
            BindInputService();
            BindProgress();
            BindSaveLoad();
            BindStaticData();
            BindRandomService();
        }

        private void BindInputService()
        {
            if (Application.isEditor)
            {
                
                
                Container
                    .Bind<IInputService>()
                    .To<StandAloneInput>()
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Container
                    .Bind<IInputService>()
                    .To<MobileInputService>()
                    .AsSingle()
                    .NonLazy();
            }
        }

        public void Initialize()
        {
        }

        private void BindAssetProvider() =>
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();

        private void BindGameFactory() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

        private void BindLoadingCurtain() =>
            Container
                .Bind<LoadingCurtain>()
                .FromComponentInNewPrefab(curtain)
                .AsSingle();


        private void BindSceneLoader() =>
            Container
                .Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();

        private void BindGameStateMachineAndStates()
        {
            Container
                .Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<StateMachineInitializer>()
                .AsSingle().NonLazy();
            Container
                .BindInterfacesAndSelfTo<BootstrapState>()
                .AsTransient();
            Container
                .BindInterfacesAndSelfTo<LoadLevelState>()
                .AsTransient();
            Container
                .BindInterfacesAndSelfTo<GameLoopState>()
                .AsTransient();
            Container
                .BindInterfacesAndSelfTo<LoadProgressState>()
                .AsTransient();
        }

        private void BindProgress() => Container
            .Bind<IPersistentProgressService>()
            .To<PersistentProgressService>()
            .AsSingle();

        private void BindSaveLoad() => 
            Container
                .Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle().NonLazy();

        private void BindStaticData() => 
            Container
                .Bind<IStaticDataService>()
                .To<StaticDataService>()
                .AsSingle().NonLazy();

        private void BindRandomService() => 
            Container
                .Bind<IRandomService>()
                .To<RandomService>()
                .AsSingle();
    }
}