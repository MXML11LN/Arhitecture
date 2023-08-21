using CodeBase.CameraLogic;
using CodeBase.Factory;
using CodeBase.GamePlay.Hero;
using CodeBase.Logic.Spawn;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI;
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
        private const string EnemySpawnerTag = "EnemySpawner";
        private const string InitialPointTag = "InitialPoint";

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

        private void CameraFollow(GameObject gameObject) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(gameObject);

        private void InitGameWorld()
        {

            InitSpawners();
            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPointTag));
            CameraFollow(hero);
            InitHud(hero);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnerGameObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                EnemySpawner spawner = spawnerGameObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InitHud(GameObject hero)
        {
            HeroHealth health = hero.GetComponent<HeroHealth>();
            GameObject hud = _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>().Construct(health);
        }
    }
}