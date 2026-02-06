using CodeBase.Gameplay;

namespace CodeBase.Infrastructure.Services.Input
{
  public interface IInputService
  {
    bool TryOpenButtonUp();
    bool MarkFieldButtonUp();
    CellView GetMouseClickCell();
  }
}