using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class WinState : IGameState
    {
        public void Enter()
        {
            Debug.Log("Entering WinState");
        }

        public void Exit()
        {
        }
    }
}
