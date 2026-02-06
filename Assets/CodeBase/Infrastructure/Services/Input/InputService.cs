using CodeBase.Gameplay;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure.Services.Input
{
  public class InputService : IInputService
  {
    public bool TryOpenButtonUp()
    {
      return SimpleInput.GetMouseButtonUp(0);
    }
    public bool MarkFieldButtonUp()
    {
      return SimpleInput.GetMouseButtonUp(1);
    }
    public bool ResetButtonUp()
    {
      return UnityEngine.Input.GetKeyUp(KeyCode.Space);
    }

    public CellView GetMouseClickCell()
    {
      Vector3 screenPos = UnityEngine.Input.mousePosition;
      Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
      Vector2 point = new Vector2(worldPos.x, worldPos.y);
      CellView target = null;
      RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
      if (hit.collider != null && hit.collider.tag == "Cell")
      {
        CellView cell = hit.collider.GetComponent<CellView>();
        if (cell != null)
          target = cell;
      }
      return target;
    }
  }
}