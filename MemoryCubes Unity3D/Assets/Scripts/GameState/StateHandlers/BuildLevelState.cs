using UnityEngine;
using System;

public class BuildCubeState : GameStateHandler
{
    private Builder builder;

    public static event Action BuildCubeStateStartedEvent;

    public BuildCubeState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        builder = GameObject.Find("Grid").GetComponent<Builder>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("BuildCubeState:GameStateStarted");

        builder.BuilderReadyEvent += OnBuilderReady;

        BuildCubeStateStartedEvent();
    }

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        Debug.Log("BuildCubeState:OnBuilderReady");

        builder.BuilderReadyEvent -= OnBuilderReady;

        GameStateFinished(GameStateEventEnum.cubeBuildingReady);
    }
}