using CodeBase.Infrastructure.Common;

namespace CodeBase.Infrastructure.Services.Match
{
    public interface IGameBoardModel
    {
        CellData[,] GameBoard { get; }
        int CountMarketElements { get; }
        int CountOpenedElements { get; }
        int Width { get; }
        int Height { get; }
        int BombCount { get; }
        void SetGameBoard(CellData[,] gameBoard);
        void UpdateCountMarketElements(int count);
        void UpdateCountOpenedElements(int count);
        void UpdateWidth(int count);
        void UpdateHeight(int count);
        void UpdateCountBombCount(int count);
    }
}