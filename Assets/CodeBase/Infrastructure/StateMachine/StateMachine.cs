using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public interface IStateMachine
    {
        IGameState Current { get; }
        event Action<IGameState> StateChanged;
        void SetState<TState>() where TState : class, IGameState;
        void ForceSetState<TState>() where TState : class, IGameState;
    }

    public class StateMachine : IStateMachine
    {
        private readonly IGameStateFactory _stateFactory;
        private readonly Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();
        private readonly TickableManager _tickableManager;
        private readonly HashSet<ITickable> _registeredTickables = new HashSet<ITickable>();

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

            SwitchState(nextState);
        }

        public void ForceSetState<TState>() where TState : class, IGameState
        {
            if (!_states.TryGetValue(typeof(TState), out IGameState nextState))
            {
                nextState = _stateFactory.Create<TState>();
                _states.Add(typeof(TState), nextState);
            }

            if (ReferenceEquals(Current, nextState))
            {
                Current?.Exit();
                Current?.Enter();
                StateChanged?.Invoke(Current);
                return;
            }

            SwitchState(nextState);
        }

        private void SwitchState(IGameState nextState)
        {
            if (Current is ITickable previousTickable)
                RemoveTickable(previousTickable);

            Current?.Exit();
            Current = nextState;
            Current.Enter();
            if (Current is ITickable currentTickable)
                AddTickable(currentTickable);
            StateChanged?.Invoke(Current);
        }

        private void AddTickable(ITickable tickable)
        {
            if (_registeredTickables.Add(tickable))
                _tickableManager.Add(tickable);
        }

        private void RemoveTickable(ITickable tickable)
        {
            if (_registeredTickables.Remove(tickable))
                _tickableManager.Remove(tickable);
        }
    }
}
