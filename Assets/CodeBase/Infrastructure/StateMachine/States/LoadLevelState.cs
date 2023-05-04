using System;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName )
        {
            _sceneLoader.LoadScene(sceneName , onLoaded: OnLoaded);
        }

        private void OnLoaded()
        {
            
        }

        public void Exit()
        {
           
        }
    }
}