using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type,IExitableState> _states;
        private IExitableState _currentState;
        
        public GameStateMachine()
        {
            _states = new Dictionary<Type, IExitableState>();
        }

        public void RegisterState(IExitableState state) => 
            _states.Add(state.GetType(),state);

        public void Enter<TState,TPayload>( TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = CastState<TState>();
            _currentState = state;
            return state;
        }

        private TState CastState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}