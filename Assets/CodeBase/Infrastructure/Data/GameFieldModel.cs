using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Match
{
    public class GameBoardModel : IGameBoardModel
    {
        private CellData[,] _gameBoard;
        public CellData[,] GameBoard => _gameBoard;
        public void SetGameBoard(CellData[,] gameBoard) => _gameBoard = gameBoard;
    }
}
