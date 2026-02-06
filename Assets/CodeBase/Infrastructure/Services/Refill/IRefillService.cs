using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Refill
{
    public interface IRefillService
    {
        CellData[,] CreateEmptyGameField(Vector2Int size);
        CellData[,] ReFillGameField(CellData[,] field, Vector2Int clickPosition, int countMines);
    }
}