using UnityEngine;
using System;

public class GameOverState : GameState
{
    public static event Action GameOverStateStartedEvent;

    private GameOverView gameOverView;

    public GameOverState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        gameOverView = uiController.GetView<GameOverView>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        gameOverView.HideCompleteEvent += OnGameOverHideFinished;

        if (GameOverStateStartedEvent != null)
        {
            GameOverStateStartedEvent();
        }
    }

    private void OnGameOverHideFinished(UIView obj)
    {
        gameOverView.HideCompleteEvent -= OnGameOverHideFinished;

        GameStateFinished(GameStateEvent.backToMenu);
    }
}