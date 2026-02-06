using CodeBase.Infrastructure.Common;

namespace CodeBase.Infrastructure.Services.Match
{
    public interface IGameBoardModel
    {
        CellData[,] GameBoard { get; }
        int CountMarketElements { get; }
        int CountOpenedElements { get; }
        void SetGameBoard(CellData[,] gameBoard);
        void UpdateCountMarketElements(int count);
        void UpdateCountOpenedElements(int count);
    }
}