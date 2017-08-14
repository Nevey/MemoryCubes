using UnityEngine;
using System;

public class BuildGridState : GameStateHandler
{
    private Builder builder;

    public static event Action BuildGridStateStartedEvent;

    public BuildGridState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("BuildCubeState:GameStateStarted");

        Builder.BuilderReadyEvent += OnBuilderReady;

        BuildGridStateStartedEvent();
    }

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        Debug.Log("BuildCubeState:OnBuilderReady");

        Builder.BuilderReadyEvent -= OnBuilderReady;

        GameStateFinished(GameStateEventEnum.cubeBuildingFinished);
    }
}