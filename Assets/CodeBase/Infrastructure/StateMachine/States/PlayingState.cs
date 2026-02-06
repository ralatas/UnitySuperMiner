using CodeBase.Gameplay;
using System.Linq;
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
                    int newCountMarketElements = cellData.IsMark ? _gameBoardModel.CountMarketElements + 1 : _gameBoardModel.CountMarketElements -1;
                    _gameBoardModel.UpdateCountMarketElements(newCountMarketElements);
                    _gameBoardViewService.MarkCell(cellData);
                    TryWinGame();
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
                        _stateMachine.SetState<LoseState>();
                    }
                    else
                    {
                        TryWinGame();
                    }
                    
                }
            }
        }

        private void TryWinGame()
        {
            int countElement = _gameBoardModel.GameBoard.Length;
            // Debug.Log("countElement " + countElement);
            // Debug.Log("countOpenedSafeCells " +_gameBoardModel.CountOpenedElements);
            // Debug.Log("countMarketElements " + _gameBoardModel.CountMarketElements);
            if (countElement - _gameBoardModel.CountOpenedElements == _gameBoardModel.CountMarketElements)
            {
                _stateMachine.SetState<WinState>();
            }
        }
    }
}
