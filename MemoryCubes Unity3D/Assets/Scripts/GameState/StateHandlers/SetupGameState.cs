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

        Debug.Log("SetupGameState:GameStateStarted");

        SetupGameStateStartedEvent();

        GameStateFinished();
    }

    private void GameStateFinished()
    {
        Debug.Log("SetupGameState:GameStateFinished");

        GameStateFinished(GameStateEvent.setupGameStateFinished);
    }
}