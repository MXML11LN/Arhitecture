namespace CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : IState;
    }
}