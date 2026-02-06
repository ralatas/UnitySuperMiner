using System;
using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Match;
using CodeBase.Infrastructure.Services.Refill;
using CodeBase.Infrastructure.Services.View;
using CodeBase.Infrastructure.Services.Assets;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States

{
    public class ReadyState : IGameState, ITickable
    {
        private const string BoardContainerKey = "BoardContainer";
        private const string CellPrefabKey = "CellPrefab";
        private readonly IRefillService _refillService;
        private readonly IGameBoardModel _gameBoardModel;
        private readonly IGameBoardViewService _gameBoardViewService;
        private readonly IInputService _inputService;
        private readonly IStateMachine _stateMachine;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IMatchService _matchService;
        private readonly IUIGameState _uiGameState;

        public ReadyState(
            IRefillService  refillService,
            IGameBoardModel  gameBoardModel, 
            IGameBoardViewService  gameBoardViewService, 
            IInputService inputService, 
            IStateMachine  stateMachine,
            IMatchService matchService,
            IUIGameState  uiGameState,
            IAssetsProvider assetsProvider)
        {
            _uiGameState = uiGameState;
            _matchService = matchService;
            _stateMachine = stateMachine;
            _inputService = inputService;
            _gameBoardViewService = gameBoardViewService;
            _gameBoardModel = gameBoardModel;
            _refillService = refillService;
            _assetsProvider = assetsProvider;
        }
        public void Enter()
        {
            CleanBoard();
            _uiGameState.SetPlayingGameState();
            CellData[,] emptyGameBoard = _refillService.CreateEmptyGameboard(_gameBoardModel.Width, _gameBoardModel.Height);
            _gameBoardModel.SetGameBoard(emptyGameBoard);
            _gameBoardModel.UpdateCountOpenedElements(0);
            _gameBoardModel.UpdateCountMarketElements(0);

            if (!_assetsProvider.TryGet(BoardContainerKey, out Transform boardContainer) ||
                !_assetsProvider.TryGet(CellPrefabKey, out GameObject cellPrefab))
            {
                Debug.LogError("Game board assets are not registered.");
                return;
            }
            _gameBoardViewService.RenderListCells(boardContainer, cellPrefab, new Vector2(0.5f, 0.5f));
            Debug.Log("Entering Game Board");
        }
        private void CleanBoard()
        {
            if (_gameBoardModel.GameBoard != null)
            {
                int width = _gameBoardModel.GameBoard.GetLength(0);
                int height = _gameBoardModel.GameBoard.GetLength(1);
                for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    _gameBoardViewService.DestroyCell(_gameBoardModel.GameBoard[x, y]);
                }
            }
        }
        public void Exit()
        {
            Debug.Log("Exiting Game Board");
        }
        public void Tick()
        {
            if (_inputService.TryOpenButtonUp()) OpenFirstCell();
            if (_inputService.MarkFieldButtonUp()) MarkCell();
        }
        private void OpenFirstCell()
        {
            CellView cellView = _inputService.GetMouseClickCell();
            if (cellView != null) {
                CellData cellData = CreateNewGameboard(cellView);
                cellData.IsOpen = true;
                _gameBoardViewService.OpenCell(cellData);
                _matchService.TryOpenNearSimilarField(cellData);
                _stateMachine.SetState<PlayingState>();
            }
        }
        private void MarkCell()
        {
            CellView cellView = _inputService.GetMouseClickCell();
            if (cellView != null)
            {
                CellData cellData = CreateNewGameboard(cellView);
                cellData.IsMark = !cellData.IsMark;
    
                int newCountMarketElements = cellData.IsMark ? _gameBoardModel.CountMarketElements + 1 : _gameBoardModel.CountMarketElements -1;
                _gameBoardModel.UpdateCountMarketElements(newCountMarketElements);
                _gameBoardViewService.MarkCell(cellData);
                _stateMachine.SetState<PlayingState>();
            }
        }
        private CellData CreateNewGameboard(CellView cellView)
        {
            CellData[,]gameBoard =_refillService.ReFillGameboard(_gameBoardModel.GameBoard, cellView.WorldPosition, _gameBoardModel.BombCount);
            CellData cellData = gameBoard[cellView.WorldPosition.x, cellView.WorldPosition.y]; 
            _gameBoardModel.SetGameBoard(gameBoard);
            return cellData;
        }
    }
}
