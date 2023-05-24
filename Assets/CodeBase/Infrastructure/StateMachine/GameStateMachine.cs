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
        
        [Inject]
        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this,sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this,sceneLoader),
            };
        }

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