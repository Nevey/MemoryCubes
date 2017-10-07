using UnityEngine;
using System;

public class StartGameState : GameStateHandler
{
    public static event Action StartGameStateStartedEvent;

    public StartGameState(GameStateEnum gameStateEnum) : base(gameStateEnum)
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

        GameStateFinished(GameStateEventEnum.startGameStateFinished);
    }
}