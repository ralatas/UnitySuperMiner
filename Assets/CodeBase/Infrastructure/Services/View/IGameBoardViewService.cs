using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.Match;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.View
{
    public interface IGameBoardViewService
    {
        void RenderListCells(Transform parent, GameObject cellPrefab, Vector2 cellSize);
        void Clear();
        void OpenCell(CellData cellData);
    }
}
