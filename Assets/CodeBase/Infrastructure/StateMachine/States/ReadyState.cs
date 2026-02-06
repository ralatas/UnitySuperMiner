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
    public interface IEnterStatePayload : TPayload
    {
        Transform Parent{ get; set; }
        GameObject Prefub { get; set; }
    }

    public class EnterStatePayload : IEnterStatePayload
    {
        public Transform Parent { get; set; }
        public GameObject Prefub { get; set; }
    }

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

        public ReadyState(
            IRefillService  refillService,
            IGameBoardModel  gameBoardModel, 
            IGameBoardViewService  gameBoardViewService, 
            IInputService inputService, 
            IStateMachine  stateMachine,
            IMatchService matchService,
            IAssetsProvider assetsProvider)
        {
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
          
            Vector2Int size = new Vector2Int(10, 5);
            CellData[,] emptyGameBoard = _refillService.CreateEmptyGameboard(size);
            _gameBoardModel.SetGameBoard(emptyGameBoard);
            if (!_assetsProvider.TryGet(BoardContainerKey, out Transform boardContainer) ||
                !_assetsProvider.TryGet(CellPrefabKey, out GameObject cellPrefab))
            {
                Debug.LogError("Game board assets are not registered.");
                return;
            }
            _gameBoardViewService.RenderListCells(boardContainer, cellPrefab, new Vector2(0.5f, 0.5f));
            Debug.Log("Entering Game Board");
        }

        public void Exit()
        {
            Debug.Log("Exiting Game Board");
        }

        public void Tick()
        {
            if (_inputService.TryOpenButtonUp())
            {
                OpenFirstCell();
            }

            if (_inputService.MarkFieldButtonUp())
            {
                MarkCell();
            }
        }

        private void OpenFirstCell()
        {
            CellView cellView = _inputService.GetMouseClickCell();
            if (cellView != null) {
                var cellData = CreateNewGameboard(cellView);
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
                var cellData = CreateNewGameboard(cellView);
                Debug.Log("Marking cell " + cellView.WorldPosition.x + " " + cellView.WorldPosition.y);
                cellData.IsMark = !cellData.IsMark;
                _gameBoardViewService.MarkCell(cellData);
                _stateMachine.SetState<PlayingState>();
            }
        }

        private CellData CreateNewGameboard(CellView cellView)
        {
            CellData[,]gameBoard =_refillService.ReFillGameboard(
                _gameBoardModel.GameBoard,
                cellView.WorldPosition, 
                5);
            CellData cellData = gameBoard[cellView.WorldPosition.x, cellView.WorldPosition.y]; 
            _gameBoardModel.SetGameBoard(gameBoard);
            return cellData;
        }
    }
}
