using CodeBase.Infrastructure.StateMachine.States;

namespace CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateFactory
    {
        TState Create<TState>() where TState : class, IGameState;
    }
}
