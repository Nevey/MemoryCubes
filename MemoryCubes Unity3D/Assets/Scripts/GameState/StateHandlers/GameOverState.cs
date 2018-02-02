using UnityEngine;
using System;

public class GameOverState : GameState2
{
    private GameOverView gameOverView;

    public GameOverState(StateID stateID) : base(stateID)
    {
        gameOverView = uiController.GetView<GameOverView>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        gameOverView.HideCompleteEvent += OnGameOverHideFinished;
    }

    private void OnGameOverHideFinished(UIView obj)
    {
        gameOverView.HideCompleteEvent -= OnGameOverHideFinished;

        GameStateFinished(GameStateEvent.backToMenu);
    }
}