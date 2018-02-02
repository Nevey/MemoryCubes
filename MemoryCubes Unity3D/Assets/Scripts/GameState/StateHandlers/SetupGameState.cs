using UnityEngine;
using System;

public class SetupGameState : GameState
{
    public SetupGameState(GameStateID gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        GameStateFinished();
    }

    private void GameStateFinished()
    {
        GameStateFinished(GameStateEvent.setupGameStateFinished);
    }
}