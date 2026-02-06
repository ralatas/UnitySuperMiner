using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.Assets
{
    public class AssetsProviderInitializer : IInitializable
    {
        private const string BoardContainerKey = "BoardContainer";
        private const string CellPrefabKey = "CellPrefab";

        private readonly IAssetsProvider _assetsProvider;
        private readonly Transform _boardContainer;
        private readonly GameObject _cellPrefab;

        public AssetsProviderInitializer(
            IAssetsProvider assetsProvider,
            [Inject(Id = BoardContainerKey)] Transform boardContainer,
            [Inject(Id = CellPrefabKey)] GameObject cellPrefab)
        {
            _assetsProvider = assetsProvider;
            _boardContainer = boardContainer;
            _cellPrefab = cellPrefab;
        }

        public void Initialize()
        {
            _assetsProvider.Set(BoardContainerKey, _boardContainer);
            _assetsProvider.Set(CellPrefabKey, _cellPrefab);
        }
    }
}
