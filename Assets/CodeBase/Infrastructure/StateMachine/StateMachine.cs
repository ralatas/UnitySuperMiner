using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public interface TPayload
    {
    }

    public interface IStateMachine
    {
        IGameState Current { get; }
        event Action<IGameState> StateChanged;
        void SetState<TState>() where TState : class, IGameState;
    }

    public class StateMachine : IStateMachine
    {
        private readonly IGameStateFactory _stateFactory;
        private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
        private readonly TickableManager _tickableManager;

        public StateMachine(IGameStateFactory stateFactory, TickableManager tickableManager)
        {
            _stateFactory = stateFactory;
            _tickableManager = tickableManager;
        }

        public IGameState Current { get; private set; }
        public event Action<IGameState> StateChanged;

        public void SetState<TState>() where TState : class, IGameState
        {
            if (!_states.TryGetValue(typeof(TState), out IGameState nextState))
            {
                nextState = _stateFactory.Create<TState>();
                _states.Add(typeof(TState), nextState);
            }

            if (ReferenceEquals(Current, nextState))
                return;

            if (Current is ITickable previousTickable)
                _tickableManager.Remove(previousTickable);

            Current?.Exit();
            Current = nextState;
            Current.Enter();
            if (Current is ITickable currentTickable)
                _tickableManager.Add(currentTickable);
            StateChanged?.Invoke(Current);
        }
    }
}
