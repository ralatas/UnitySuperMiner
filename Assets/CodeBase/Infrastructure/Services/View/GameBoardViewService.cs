using System.Collections.Generic;
using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.Match;
using CodeBase.Infrastructure.Services.View.CellFactory;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.View
{
    public class GameBoardViewService : IGameBoardViewService
    {
        private readonly List<CellView> _spawnedCells = new List<CellView>();
        private readonly IGameBoardModel _gameBoardModel;
        private readonly ICellViewFactory _cellViewFactory;

        public GameBoardViewService(IGameBoardModel gameBoardModel, ICellViewFactory cellViewFactory)
        {
            _gameBoardModel = gameBoardModel;
            _cellViewFactory = cellViewFactory;
        }
        public void RenderListCells(Transform parent, GameObject cellPrefab, Vector2 cellSize)
        {
            Clear();

            if (_gameBoardModel == null || _gameBoardModel.GameBoard == null || parent == null || cellPrefab == null)
                return;
            
            int width = _gameBoardModel.GameBoard.GetUpperBound(0) + 1;
            int height = _gameBoardModel.GameBoard.GetUpperBound(1) + 1;
            float offsetX = -((width - 1) * cellSize.x) * 0.5f;
            float offsetY = -((height - 1) * cellSize.y) * 0.5f;

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vector3 localPosition = new Vector3(offsetX + x * cellSize.x, offsetY + y * cellSize.y, 0f);
                CellView cell = _cellViewFactory.Create(cellPrefab, parent).GetComponent<CellView>();
                cell.transform.localPosition = localPosition;
                cell.WorldPosition = new Vector2Int(x, y);
                _gameBoardModel.GameBoard[x, y].View = cell;
                _spawnedCells.Add(cell);
            }
        }

        public void OpenCell(CellData cellData)
        {
            SpriteRenderer renderer = cellData.View.GetComponent<SpriteRenderer>();
            if (cellData.Value == -1)
            {
                renderer.sprite = cellData.View.bombRedSprite;
            }
            else
            {
                renderer.sprite = cellData.View.NearBombSprite[cellData.Value];
            }
        }

        public void MarkCell(CellData cellData)
        {
            SpriteRenderer renderer = cellData.View.GetComponent<SpriteRenderer>();
            renderer.sprite = cellData.IsMark ? cellData.View.flagSprite: cellData.View.closedSprite ;
        }

        public void Clear()
        {
            for (int i = 0; i < _spawnedCells.Count; i++)
            {
                if (_spawnedCells[i] != null)
                    Object.Destroy(_spawnedCells[i]);
            }

            _spawnedCells.Clear();
        }
    }
}
