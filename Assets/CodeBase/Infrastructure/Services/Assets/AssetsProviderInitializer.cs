using CodeBase.Meta;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.Assets
{
    public class AssetsProviderInitializer : IInitializable
    {
        private const string BoardContainerKey = "BoardContainer";
        private const string CellPrefabKey = "CellPrefab";
        private const string UiGameStateViewKey = "UIGameStateView";

        private readonly IAssetsProvider _assetsProvider;
        private readonly Transform _boardContainer;
        private readonly GameObject _cellPrefab;
        private readonly UIGameStateView _uiGameStateView;

        public AssetsProviderInitializer(
            IAssetsProvider assetsProvider,
            [Inject(Id = BoardContainerKey)] Transform boardContainer,
            [Inject(Id = CellPrefabKey)] GameObject cellPrefab,
            [InjectOptional] UIGameStateView uiGameStateView)
        {
            _assetsProvider = assetsProvider;
            _boardContainer = boardContainer;
            _cellPrefab = cellPrefab;
            _uiGameStateView = uiGameStateView;
        }

        public void Initialize()
        {
            _assetsProvider.Set(BoardContainerKey, _boardContainer);
            _assetsProvider.Set(CellPrefabKey, _cellPrefab);
            if (_uiGameStateView != null)
                _assetsProvider.Set(UiGameStateViewKey, _uiGameStateView);
        }
    }
}
