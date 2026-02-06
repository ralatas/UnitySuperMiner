using CodeBase.Infrastructure.Services.Match;
using CodeBase.Infrastructure.Services.View;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoseState : IGameState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IGameBoardModel _gameBoardModel;
        private readonly IGameBoardViewService _gameBoardViewService;
        private readonly IUIGameState _uiGameState;

        public LoseState(
            IStateMachine stateMachine,
            IGameBoardModel gameBoardModel,
            IUIGameState  uiGameState,
            IGameBoardViewService gameBoardViewService)
        {
            _uiGameState = uiGameState;
            _gameBoardViewService = gameBoardViewService;
            _gameBoardModel = gameBoardModel;
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _uiGameState.SetFailGameState();
            _gameBoardViewService.ShowBombs();
            
        }

        public void Exit()
        {
        }
    }
}
