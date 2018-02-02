using UnityEngine;
using System;

public class GameOverState : GameState
{
    private GameOverView gameOverView;

    public GameOverState(GameStateID gameStateEnum) : base(gameStateEnum)
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