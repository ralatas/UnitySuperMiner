
using System;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

public class GameBootstraper : MonoBehaviour
{
    private IStateMachine _stateMachine;
    
    [Inject]
    private void Construct(IStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    void Start()
    {
        _stateMachine.SetState<ReadyState>();
    }
    
}
