using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Match
{
    public class GameBoardModel : IGameBoardModel
    {
        private CellData[,] _gameBoard;
        private int _countMarketElements = 0;
        private int _countOpenedElements = 0;
        public CellData[,] GameBoard => _gameBoard;
        public int CountMarketElements => _countMarketElements;
        public int CountOpenedElements => _countOpenedElements;
        public void SetGameBoard(CellData[,] gameBoard) => _gameBoard = gameBoard;
        public void UpdateCountMarketElements(int count) => _countMarketElements = count;
        public void UpdateCountOpenedElements(int count) => _countOpenedElements = count;
    }
}
