using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.View;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly IGameBoardModel _gameBoardModel;
        private readonly IGameBoardViewService _gameBoardViewService;

        public MatchService(IGameBoardModel  gameBoardModel, IGameBoardViewService gameBoardViewService )
        {
            _gameBoardViewService = gameBoardViewService;
            _gameBoardModel = gameBoardModel;
        }
        public void TryOpenNearSimilarField(CellData cellData)
        {
            int width = _gameBoardModel.GameBoard.GetUpperBound(0) + 1;
            int height = _gameBoardModel.GameBoard.GetUpperBound(1) + 1;
            
            if (cellData.Value == 0)
            {
                RecursiveSearchEmptyCell(
                    _gameBoardModel.GameBoard,
                    cellData,
                    width, 
                    height);
            }
        }

        private void RecursiveSearchEmptyCell(
            CellData[,] boardData,
            CellData cellData, 
            int width, 
            int height)
        {
            int x = cellData.View.WorldPosition.x;
            int y = cellData.View.WorldPosition.y;
            
            int left = cellData.View.WorldPosition.x - 1;
            int right = cellData.View.WorldPosition.x + 1;
            int down = cellData.View.WorldPosition.y - 1;
            int up = cellData.View.WorldPosition.y + 1;

            if (left >= 0 && boardData[left, y].Value == 0)
            {
                CellData nextCell = boardData[left, y];
                OpenNearCell(boardData, cellData, width, height, nextCell);
            }

            if (right < width && boardData[right, y].Value == 0)
            {
                CellData nextCell = boardData[right, y];
                OpenNearCell(boardData, cellData, width, height, nextCell);
            }
            
            if (down >= 0 && boardData[x, down].Value == 0)
            {
                CellData nextCell = boardData[x, down];
                OpenNearCell(boardData, cellData, width, height, nextCell);
            }

            if (up < height && boardData[x, up].Value == 0)
            {
                CellData nextCell = boardData[x, up];
                OpenNearCell(boardData, cellData, width, height, nextCell);
            }
        }

        private void OpenNearCell(CellData[,] boardDatas, CellData cellData, int width, int height, CellData nextCell)
        {
            nextCell.IsOpen = true;
            _gameBoardViewService.OpenCell(cellData);
            RecursiveSearchEmptyCell(boardDatas, nextCell, width, height);
        }
    }
}
