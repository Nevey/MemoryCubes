using UnityEngine;
using System;

public class MainMenuState : GameStateHandler
{
    public static event Action MainMenuStateStartedEvent;

    public MainMenuState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("MainMenuState:GameStateStarted");

        MainMenuStateStartedEvent();
    }

    private void GameStateFinished()
    {
        Debug.Log("MainMenuState:GameStateFinished");

        // GameStateFinished(GameStateEventEnum.setupGameStateFinished);
    }
}