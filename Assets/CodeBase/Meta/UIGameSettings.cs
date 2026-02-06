using System;
using System.Collections;
using CodeBase.Infrastructure.Services.Match;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Meta
{
    public class UIGameSettings: MonoBehaviour
    {
        private static IGameBoardModel _gameBoardModel;
        private static IStateMachine _stateMachine;
        [SerializeField] public TextMeshProUGUI bombCountView;
        [SerializeField] public TextMeshProUGUI widthView;
        [SerializeField] public TextMeshProUGUI heightView;
        [SerializeField] public Slider BombCountValue;
        [SerializeField] public Slider WidthValue;
        [SerializeField] public Slider HeightValue;
        [SerializeField] public Button StartButton;

        [Inject]
        private void Construct(IStateMachine stateMachine, IGameBoardModel gameBoardModel)
        {
            _gameBoardModel = gameBoardModel;
            _stateMachine = stateMachine;
        }
        private void Awake()
        {
            bombCountView.text = _gameBoardModel.BombCount.ToString();
            widthView.text = _gameBoardModel.Width.ToString();
            heightView.text = _gameBoardModel.Height.ToString();
            BombCountValue.value = _gameBoardModel.BombCount;
            WidthValue.value = _gameBoardModel.Width;
            HeightValue.value = _gameBoardModel.Height;
            BombCountValue.onValueChanged.AddListener(UpdateBombView);
            WidthValue.onValueChanged.AddListener(UpdateWidthView);
            HeightValue.onValueChanged.AddListener(UpdateHeightView);
            StartButton.onClick.AddListener(StartGameWithNewParams);
        }

        private void StartGameWithNewParams()
        {
            _gameBoardModel.UpdateWidth((int)WidthValue.value);
            _gameBoardModel.UpdateHeight((int)HeightValue.value);
            _gameBoardModel.UpdateCountBombCount((int)BombCountValue.value);
            _stateMachine.ForceSetState<ReadyState>();
        }
        private void UpdateBombView(float arg0)
        {
            bombCountView.text = arg0.ToString("0");
        }
        private void UpdateWidthView(float arg0)
        {
            BombCountValue.maxValue = arg0 * HeightValue.value - 1;
            widthView.text = arg0.ToString("0");
        }
        private void UpdateHeightView(float arg0)
        {
            BombCountValue.maxValue = arg0 * WidthValue.value - 1;
            heightView.text = arg0.ToString("0");
        }
    }
}