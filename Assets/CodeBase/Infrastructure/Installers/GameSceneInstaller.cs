using CodeBase.Infrastructure.Services.Assets;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform BoardContainer;
        [SerializeField] private GameObject CellPrefab;

        public override void InstallBindings()
        {
            BindSceneObject();
        }
        
        private void BindSceneObject()
        {
            Container.BindInstance(BoardContainer).WithId("BoardContainer");
            Container.BindInstance(CellPrefab).WithId("CellPrefab");
            Container.BindInterfacesTo<AssetsProviderInitializer>().AsSingle();
        }
    }
}
