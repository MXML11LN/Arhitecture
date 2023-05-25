using CodeBase.CameraLogic;
using CodeBase.Factory;
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

        [Inject]
        public LoadLevelState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader,LoadingCurtain curtain,IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName )
        {
            _curtain.Show();
            _sceneLoader.LoadScene(sceneName , onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
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