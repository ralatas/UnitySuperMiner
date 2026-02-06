using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.Match;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.View
{
    public interface IGameBoardViewService
    {
        void RenderListCells(Transform parent, GameObject cellPrefab, Vector2 cellSize);
        void OpenCell(CellData cellData);
        void MarkCell(CellData cellData);
        void ShowBombs();
        void DestroyCell(CellData cellData);
    }
}
