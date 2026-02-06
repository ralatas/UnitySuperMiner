using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Refill
{
    public class RefillService : IRefillService
    {
        public CellData[,] CreateEmptyGameboard(Vector2Int size)
        {
            int width = size.x;
            int height = size.y;
            CellData[,] field = new CellData[width, height];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                field[x, y] = new CellData { Value = 0, IsOpen = false };
            }
            return field;
        }
        public CellData[,] ReFillGameboard(CellData[,] gameBoard, Vector2Int clickPosition, int countMines)
        {
            int width = gameBoard.GetUpperBound(0) + 1;
            int height = gameBoard.GetUpperBound(1) + 1;
            const int MineValue = -1;

            bool hasSafeCell = clickPosition.x >= 0 && clickPosition.x < width &&
                               clickPosition.y >= 0 && clickPosition.y < height;
            int totalCells = width * height;
            int availableCells = totalCells - (hasSafeCell ? 1 : 0);
            int remainingMines = Mathf.Clamp(countMines, 0, availableCells);
            int remainingCells = availableCells;

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (hasSafeCell && x == clickPosition.x && y == clickPosition.y)
                    continue;

                int roll = Random.Range(0, remainingCells);
                if (roll < remainingMines)
                {
                    gameBoard[x, y].Value = MineValue;
                    remainingMines--;

                    int left = x - 1;
                    int right = x + 1;
                    int down = y - 1;
                    int up = y + 1;

                    if (left >= 0)
                    {
                        if (gameBoard[left, y].Value != MineValue) gameBoard[left, y].Value++;
                        if (down >= 0 && gameBoard[left, down].Value != MineValue) gameBoard[left, down].Value++;
                        if (up < height && gameBoard[left, up].Value != MineValue) gameBoard[left, up].Value++;
                    }

                    if (right < width)
                    {
                        if (gameBoard[right, y].Value != MineValue) gameBoard[right, y].Value++;
                        if (down >= 0 && gameBoard[right, down].Value != MineValue) gameBoard[right, down].Value++;
                        if (up < height && gameBoard[right, up].Value != MineValue) gameBoard[right, up].Value++;
                    }

                    if (down >= 0 && gameBoard[x, down].Value != MineValue) gameBoard[x, down].Value++;
                    if (up < height && gameBoard[x, up].Value != MineValue) gameBoard[x, up].Value++;
                }

                remainingCells--;
            }

            return gameBoard;
        }
    }
}
