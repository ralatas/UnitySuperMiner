using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Match
{
    public class GameBoardModel : IGameBoardModel
    {
        private CellData[,] _gameBoard;
        private int _countMarketElements = 0;
        private int _countOpenedElements = 0;
        private int _width = 5;
        private int _height = 5;
        private int _bombCount = 5;
        
        public CellData[,] GameBoard => _gameBoard;
        public int CountMarketElements => _countMarketElements;
        public int CountOpenedElements => _countOpenedElements;
        public int Width => _width;
        public int Height => _height;
        public int BombCount => _bombCount;

        public void SetGameBoard(CellData[,] gameBoard) => _gameBoard = gameBoard;
        public void UpdateCountMarketElements(int count) => _countMarketElements = count;
        public void UpdateCountOpenedElements(int count) => _countOpenedElements = count;
        public void UpdateWidth(int count) => _width = count;
        public void UpdateHeight(int count) => _height = count;
        public void UpdateCountBombCount(int count) => _bombCount = count;
    }
}
