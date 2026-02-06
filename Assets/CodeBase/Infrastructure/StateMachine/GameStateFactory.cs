using CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly DiContainer _container;

        public GameStateFactory(DiContainer container)
        {
            _container = container;
        }

        public TState Create<TState>() where TState : class, IGameState
        {
            return _container.Instantiate<TState>();
        }
    }
}
