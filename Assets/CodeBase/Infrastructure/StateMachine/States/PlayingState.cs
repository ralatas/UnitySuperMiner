using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Match;
using CodeBase.Infrastructure.Services.View;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class PlayingState : IGameState, ITickable
    {
        private readonly IInputService _inputService;
        private readonly IGameBoardModel _gameBoardModel;
        private readonly IGameBoardViewService _gameBoardViewService;
        private readonly IMatchService _matchService;
        private readonly IStateMachine _stateMachine;

        public PlayingState(
            IInputService inputService,
            IGameBoardModel  gameBoardModel, 
            IGameBoardViewService  gameBoardViewService,
            IMatchService matchService,
            IStateMachine stateMachine)
        
        {
            _stateMachine = stateMachine;
            _matchService = matchService;
            _gameBoardViewService = gameBoardViewService;
            _gameBoardModel = gameBoardModel;
            _inputService = inputService;
        }
        public void Enter()
        {
            
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            if (_inputService.TryOpenButtonUp())
                CheckCell();
            if (_inputService.MarkFieldButtonUp())
                MarkCell();
        }

        private void MarkCell()
        {
            CellView cellView = _inputService.GetMouseClickCell();
            if (cellView != null)
            {
                CellData cellData = _gameBoardModel.GameBoard[cellView.WorldPosition.x, cellView.WorldPosition.y];
                if (cellData != null && !cellData.IsOpen)
                {
                    cellData.IsMark = !cellData.IsMark;
                    _gameBoardViewService.MarkCell(cellData);
                }
            }
        }
        private void CheckCell()
        {
            CellView cellView = _inputService.GetMouseClickCell();
            if (cellView != null)
            {
                CellData cellData = _gameBoardModel.GameBoard[cellView.WorldPosition.x, cellView.WorldPosition.y];
                if (cellData != null && !cellData.IsOpen && !cellData.IsMark)
                {
                    cellData.IsOpen = true;
                    _gameBoardViewService.OpenCell(cellData);
                    _matchService.TryOpenNearSimilarField(cellData);
                    if (cellData.Value == -1)
                    { 
                        //_stateMachine.SetState<LoseState>();
                    }
                }
                //_stateMachine.SetState<PlayingState>();
            }
        }
    }
}
