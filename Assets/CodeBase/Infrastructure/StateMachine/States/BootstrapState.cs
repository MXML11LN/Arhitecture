using CodeBase.Services.StaticData;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private const string BootSceneName = "Boot";
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;

        [Inject]
        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticData = staticData;
        }

        public void Enter()
        {
            WarmUp();
            _sceneLoader.LoadScene(sceneName: BootSceneName, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void WarmUp()
        {
            _staticData.LoadMonsters();
        }

        public void Exit()
        {
        }
    }
}