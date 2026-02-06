using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoseState : IGameState
    {
        private readonly IStateMachine _stateMachine;

        public LoseState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            Debug.Log("Entering LoseState");
            _stateMachine.SetState<ReadyState>();
        }

        public void Exit()
        {
        }
    }
}
