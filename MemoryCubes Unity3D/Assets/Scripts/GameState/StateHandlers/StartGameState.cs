using UnityEngine;
using System;

public class StartGameState : GameState
{
    public static event Action StartGameStateStartedEvent;

    public StartGameState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("StartGameState:GameStateStarted");

        StartGameStateStartedEvent();

        GameStateFinished();
    }

    private void GameStateFinished()
    {
        Debug.Log("StartGameState:GameStateFinished");

        GameStateFinished(GameStateEvent.startGameStateFinished);
    }
}