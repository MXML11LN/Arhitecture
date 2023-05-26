using CodeBase.CameraLogic;
using CodeBase.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        [Inject]
        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader,LoadingCurtain curtain,IGameFactory gameFactory,IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName )
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.LoadScene(sceneName , onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void CameraFollow( GameObject gameObject) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);

        private void InitGameWorld()
        {
            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag("InitialPoint"));
            CameraFollow(hero);
            _gameFactory.CreateHUD();
        }
    }
}