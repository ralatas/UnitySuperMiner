using CodeBase.Infrastructure.Common;

namespace CodeBase.Infrastructure.Services.Match
{
    public interface IGameBoardModel
    {
        CellData[,] GameBoard { get; }
        void SetGameBoard(CellData[,] gameBoard);
    }
}