using UnityEngine;
using System;

public class SetupGameState : GameState
{
    public static event Action SetupGameStateStartedEvent;

    public SetupGameState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();
        
        SetupGameStateStartedEvent();

        GameStateFinished();
    }

    private void GameStateFinished()
    {
        GameStateFinished(GameStateEvent.setupGameStateFinished);
    }
}