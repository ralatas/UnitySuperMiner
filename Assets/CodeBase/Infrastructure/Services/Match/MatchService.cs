using System.Collections.Generic;
using CodeBase.Gameplay;
using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Services.View;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly IGameBoardModel _gameBoardModel;
        private readonly IGameBoardViewService _gameBoardViewService;

        public MatchService(IGameBoardModel  gameBoardModel, IGameBoardViewService gameBoardViewService )
        {
            _gameBoardViewService = gameBoardViewService;
            _gameBoardModel = gameBoardModel;
        }
        public void TryOpenNearSimilarField(CellData cellData)
        {
            if (cellData == null || _gameBoardModel.GameBoard == null)
                return;

            int width = _gameBoardModel.GameBoard.GetUpperBound(0) + 1;
            int height = _gameBoardModel.GameBoard.GetUpperBound(1) + 1;

            int startX = cellData.View.WorldPosition.x;
            int startY = cellData.View.WorldPosition.y;
            if (startX < 0 || startX >= width || startY < 0 || startY >= height)
                return;

            OpenEmptyCells(_gameBoardModel.GameBoard, startX, startY, width, height);
        }

        private void OpenEmptyCells(CellData[,] boardData, int startX, int startY, int width, int height)
        {
            bool[,] visited = new bool[width, height];
            Queue<Vector2Int> queue = new Queue<Vector2Int>();

            queue.Enqueue(new Vector2Int(startX, startY));
            visited[startX, startY] = true;

            while (queue.Count > 0)
            {
                Vector2Int pos = queue.Dequeue();
                CellData current = boardData[pos.x, pos.y];
                if (current == null)
                    continue;

                if (!current.IsOpen)
                {
                    current.IsOpen = true;
                    _gameBoardViewService.OpenCell(current);
                }

                if (current.Value != 0)
                    continue;

                TryEnqueue(pos.x - 1, pos.y, width, height, visited, queue);
                TryEnqueue(pos.x + 1, pos.y, width, height, visited, queue);
                TryEnqueue(pos.x, pos.y - 1, width, height, visited, queue);
                TryEnqueue(pos.x, pos.y + 1, width, height, visited, queue);
            }
        }

        private static void TryEnqueue(
            int x,
            int y,
            int width,
            int height,
            bool[,] visited,
            Queue<Vector2Int> queue)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                return;
            if (visited[x, y])
                return;

            visited[x, y] = true;
            queue.Enqueue(new Vector2Int(x, y));
        }
    }
}
