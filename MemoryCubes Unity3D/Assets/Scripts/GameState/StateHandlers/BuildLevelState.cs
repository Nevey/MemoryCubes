using UnityEngine;
using System;

public class BuildGridState : GameState
{
    private GridBuilder gridBuilder;

    public BuildGridState(GameStateID gameStateEnum) : base(gameStateEnum)
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