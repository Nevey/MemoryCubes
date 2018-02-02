using UnityEngine;
using System;

public class StartGameState : GameState
{
    public static event Action StartGameStateStartedEvent;

    public StartGameState(GameStateID gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        StartGameStateStartedEvent();

        GameStateFinished(GameStateEvent.startGameStateFinished);
    }
}