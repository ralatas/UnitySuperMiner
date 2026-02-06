using UnityEngine;

namespace CodeBase.Infrastructure.Services.View.CellFactory
{
    public interface ICellViewFactory
    {
        GameObject Create(GameObject prefab, Transform parent);
    }
}
