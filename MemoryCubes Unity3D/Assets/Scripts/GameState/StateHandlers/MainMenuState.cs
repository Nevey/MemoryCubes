using UnityEngine;
using System;

public class MainMenuState : GameState
{
    private MainMenuView mainMenuView;

    public MainMenuState(GameStateID gameStateEnum) : base(gameStateEnum)
    {
        mainMenuView = uiController.GetView<MainMenuView>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();
        
        mainMenuView.HideCompleteEvent += OnHideComplete;
    }

    private void OnHideComplete(UIView uIView)
    {
        mainMenuView.HideCompleteEvent -= OnHideComplete;

        GameStateFinished(GameStateEvent.startGame);
    }
}