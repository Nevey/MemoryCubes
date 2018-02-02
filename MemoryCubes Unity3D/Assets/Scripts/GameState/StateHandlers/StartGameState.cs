using UnityEngine;
using System;

public class StartGameState : GameState2
{
    public static event Action StartGameStateStartedEvent;

    public StartGameState(StateID stateID) : base(stateID)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        StartGameStateStartedEvent();

        GameStateFinished(GameStateEvent.startGameStateFinished);
    }
}