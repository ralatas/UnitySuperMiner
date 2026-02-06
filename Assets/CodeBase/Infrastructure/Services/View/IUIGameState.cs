namespace CodeBase.Infrastructure.Services.View
{
    public interface IUIGameState
    {
        void SetWinGameState();
        void SetFailGameState();
        void SetPlayingGameState();
    }
}