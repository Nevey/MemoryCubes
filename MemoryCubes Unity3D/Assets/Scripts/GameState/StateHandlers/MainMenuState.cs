using UnityEngine;
using System;

public class MainMenuState2 : GameState2
{
    private MainMenuView mainMenuView;

    public MainMenuState2(StateID stateID) : base(stateID)
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