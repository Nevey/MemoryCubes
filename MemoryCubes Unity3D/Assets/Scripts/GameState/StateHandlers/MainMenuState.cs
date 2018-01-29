using UnityEngine;
using System;

public class MainMenuState : GameState
{
    public static event Action MainMenuStateStartedEvent;

    private MainMenuView mainMenuView;

    public MainMenuState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        mainMenuView = uiController.GetViewByID(UIViewID.Main) as MainMenuView;
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();
        
        mainMenuView.HideCompleteEvent += OnHideComplete;

        MainMenuStateStartedEvent();
    }

    private void OnHideComplete(UIView uIView)
    {
        mainMenuView.HideCompleteEvent -= OnHideComplete;

        GameStateFinished(GameStateEvent.startGame);
    }
}