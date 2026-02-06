using CodeBase.Infrastructure.Services.View;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class WinState : IGameState
    {
        private readonly IUIGameState _uiGameState;

        public WinState(IUIGameState  uiGameState)
        {
            _uiGameState = uiGameState;
        }
        public void Enter()
        {
            Debug.Log("Entering WinState");
            _uiGameState.SetWinGameState();
        }

        public void Exit()
        {
        }
    }
}
