using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Meta;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.View
{
    public class UIGameState : IUIGameState
    {
        private const string UiGameStateViewKey = "UIGameStateView";
        private readonly IAssetsProvider _assetsProvider;
        private UIGameStateView _view;
        private readonly IStateMachine _stateMachine;

        public UIGameState(IAssetsProvider assetsProvider, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _assetsProvider = assetsProvider;
        }

        public void SetWinGameState()
        {
            if (!TryGetView(out UIGameStateView view))
                return;
            view.Title.text = "win";
            view.StateImage.sprite = view.StateImageWin;
            view.OneMoreButton.gameObject.SetActive(true);
           
        }

        public void SetFailGameState()
        {
            if (!TryGetView(out UIGameStateView view))
                return;
            view.Title.text = "game over";
            view.StateImage.sprite = view.StateImageLose;
            view.OneMoreButton.gameObject.SetActive(true);
        }

        public void SetPlayingGameState()
        {
            if (!TryGetView(out UIGameStateView view))
                return;
            view.Title.text = "ищи мины";
            view.StateImage.sprite = view.StateImageGaming;
            view.OneMoreButton.gameObject.SetActive(false);
            view.OneMoreButton.onClick.RemoveAllListeners();
            view.OneMoreButton.onClick.AddListener(() => _stateMachine.ForceSetState<ReadyState>());
        }

        private bool TryGetView(out UIGameStateView view)
        {
            if (_view == null)
                _assetsProvider.TryGet(UiGameStateViewKey, out _view);

            view = _view;
            if (view != null)
                return true;

            Debug.LogError("UIGameStateView is not registered.");
            return false;
        }
    }
}
