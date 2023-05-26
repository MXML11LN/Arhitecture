using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public class StateMachineInitializer : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly BootstrapState _bootstrapState;
        private readonly LoadLevelState _loadLevelState;
        private readonly GameLoopState _gameLoopState;
        private readonly LoadProgressState _loadProgressState;

        [Inject]
        public StateMachineInitializer(
            IGameStateMachine stateMachine,
            BootstrapState bootstrapState,
            LoadLevelState loadLevelState,
            GameLoopState gameLoopState,
            LoadProgressState loadProgressState)
        {
            _stateMachine = stateMachine;
            _bootstrapState = bootstrapState;
            _loadLevelState = loadLevelState;
            _gameLoopState = gameLoopState;
            _loadProgressState = loadProgressState;
        }
        public void Initialize()
        {
            _stateMachine.RegisterState(_bootstrapState);
            _stateMachine.RegisterState(_loadLevelState);
            _stateMachine.RegisterState(_gameLoopState);
            _stateMachine.RegisterState(_loadProgressState);
            _stateMachine.Enter<BootstrapState>();
        }
    }
}