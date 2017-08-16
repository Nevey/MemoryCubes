using UnityEngine;
using System;

public class BuildGridState : GameStateHandler
{
    public static event Action BuildGridStateStartedEvent;

    public BuildGridState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("BuildCubeState:GameStateStarted");

        GridBuilder.BuilderReadyEvent += OnBuilderReady;

        BuildGridStateStartedEvent();
    }

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        Debug.Log("BuildCubeState:OnBuilderReady");

        GridBuilder.BuilderReadyEvent -= OnBuilderReady;

        GameStateFinished(GameStateEventEnum.cubeBuildingFinished);
    }
}