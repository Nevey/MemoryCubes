using UnityEngine;
using System;

public class SetupGameState : GameState2
{
    public SetupGameState(StateID stateID) : base(stateID)
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