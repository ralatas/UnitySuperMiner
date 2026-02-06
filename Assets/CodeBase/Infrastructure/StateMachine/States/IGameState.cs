using JetBrains.Annotations;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}
