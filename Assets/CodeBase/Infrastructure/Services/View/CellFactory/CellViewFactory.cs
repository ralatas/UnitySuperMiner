using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.View.CellFactory
{
    public class CellViewFactory : ICellViewFactory
    {
        private readonly DiContainer _container;

        public CellViewFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab, Transform parent)
        {
            return _container.InstantiatePrefab(prefab, parent);
        }
    }
}
