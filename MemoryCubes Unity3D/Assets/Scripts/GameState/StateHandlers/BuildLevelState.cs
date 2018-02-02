using UnityEngine;
using System;

public class BuildGridState : GameState2
{
    private GridBuilder gridBuilder;

    public BuildGridState(StateID stateID) : base(stateID)
    {
        gridBuilder = MonoBehaviour.FindObjectOfType<GridBuilder>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        gridBuilder.BuilderReadyEvent += OnBuilderReady;
    }

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        gridBuilder.BuilderReadyEvent -= OnBuilderReady;

        GameStateFinished(GameStateEvent.cubeBuildingFinished);
    }
}