using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Match;
using CodeBase.Infrastructure.Services.Refill;
using CodeBase.Infrastructure.Services.View;
using CodeBase.Infrastructure.Services.View.CellFactory;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

public class BoostrapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputService();
        BindGameData();
    }

    private void BindGameData()
    {
        Container.Bind<IGameBoardModel>().To<GameBoardModel>().AsSingle();
        Container.Bind<IAssetsProvider>().To<AssetsProvider>().AsSingle();
        Container.Bind<ICellViewFactory>().To<CellViewFactory>().AsSingle();
        Container.Bind<IGameBoardViewService>().To<GameBoardViewService>().AsSingle();
    }

    private void BindInputService()
    {
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<IMatchService>().To<MatchService>().AsSingle();
        Container.Bind<IRefillService>().To<RefillService>().AsSingle();
        Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle();
    }
}
