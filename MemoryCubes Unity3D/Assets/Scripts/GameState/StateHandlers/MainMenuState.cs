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

        MainMenuView.StartPressedEvent += OnStartPressed;

        MainMenuStateStartedEvent();
    }

    private void OnStartPressed()
    {
        Debug.Log("MainMenuState:OnStartPressed");

        MainMenuView.StartPressedEvent -= OnStartPressed;

        GameStateFinished(GameStateEventEnum.startGame);
    }
}