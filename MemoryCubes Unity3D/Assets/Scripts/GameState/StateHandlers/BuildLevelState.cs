using UnityEngine;
using System;

public class BuildGridState : GameState2
{
    public BuildGridState(StateID stateID) : base(stateID)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        GridBuilder.Instance.BuilderReadyEvent += OnBuilderReady;
    }

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        GridBuilder.Instance.BuilderReadyEvent -= OnBuilderReady;

        GameStateFinished(GameStateEvent.cubeBuildingFinished);
    }
}