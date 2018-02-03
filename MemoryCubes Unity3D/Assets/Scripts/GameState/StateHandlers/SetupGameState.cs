using UnityEngine;
using System;

public class SetupGameState2 : GameState2
{
    public SetupGameState2(StateID stateID) : base(stateID)
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