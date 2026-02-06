using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Refill
{
    public interface IRefillService
    {
        CellData[,] CreateEmptyGameboard(Vector2Int size);
        CellData[,] ReFillGameboard(CellData[,] gameBoard, Vector2Int clickPosition, int countMines);
    }
}