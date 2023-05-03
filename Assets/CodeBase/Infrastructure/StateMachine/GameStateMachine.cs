using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type,IState> _states;
        private IState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }

        public void RegisterStates(IState state)
        {
            
        }

        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit();
            IState state = _states[typeof(TState)];
            state.Enter();
            _currentState = state;
        }
    }
}