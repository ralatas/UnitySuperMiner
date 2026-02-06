
using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

public class GameBootstraper : MonoBehaviour
{
    private static IInputService _inputService;
    private static IStateMachine _stateMachine;
    
    [Inject]
    private void Construct(IStateMachine stateMachine, IInputService inputService)
    {
        _inputService = inputService;
        _stateMachine = stateMachine;
    }

    void Start()
    {
        _stateMachine.SetState<ReadyState>();
    }

    void Update()
    {
        if (_inputService.ResetButtonUp())
            _stateMachine.ForceSetState<ReadyState>();
    }

}
