using UnityEngine;
using System;

public class StartGameState2 : GameState2
{
    public static event Action StartGameStateStartedEvent;

    public StartGameState2(StateID stateID) : base(stateID)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        StartGameStateStartedEvent();

        GameStateFinished(GameStateEvent.startGameStateFinished);
    }
}