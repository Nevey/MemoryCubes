using UnityEngine;
using System;

public class MainMenuState : GameState
{
    public static event Action MainMenuStateStartedEvent;

    public MainMenuState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("MainMenuState:GameStateStarted");

        MainMenuView.GameModePressedEvent += OnGameModePressed;

        MainMenuStateStartedEvent();
    }

    private void OnGameModePressed()
    {
        Debug.Log("MainMenuState:OnGameModePressed");

        MainMenuView.GameModePressedEvent -= OnGameModePressed;

        GameStateFinished(GameStateEvent.startGame);
    }
}