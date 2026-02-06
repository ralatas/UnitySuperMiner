using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Refill
{
    public class RefillService : IRefillService
    {
        public CellData[,] CreateEmptyGameField(Vector2Int size)
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
        public CellData[,] ReFillGameField(CellData[,] field, Vector2Int clickPosition, int countMines)
        {
            int width = field.GetUpperBound(0) + 1;
            int height = field.GetUpperBound(1) + 1;
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
                    field[x, y].Value = MineValue;
                    remainingMines--;

                    int left = x - 1;
                    int right = x + 1;
                    int down = y - 1;
                    int up = y + 1;

                    if (left >= 0)
                    {
                        if (field[left, y].Value != MineValue) field[left, y].Value++;
                        if (down >= 0 && field[left, down].Value != MineValue) field[left, down].Value++;
                        if (up < height && field[left, up].Value != MineValue) field[left, up].Value++;
                    }

                    if (right < width)
                    {
                        if (field[right, y].Value != MineValue) field[right, y].Value++;
                        if (down >= 0 && field[right, down].Value != MineValue) field[right, down].Value++;
                        if (up < height && field[right, up].Value != MineValue) field[right, up].Value++;
                    }

                    if (down >= 0 && field[x, down].Value != MineValue) field[x, down].Value++;
                    if (up < height && field[x, up].Value != MineValue) field[x, up].Value++;
                }

                remainingCells--;
            }

            return field;
        }
    }
}
