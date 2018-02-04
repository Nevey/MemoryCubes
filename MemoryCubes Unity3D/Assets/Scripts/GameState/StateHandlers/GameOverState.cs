using UnityEngine;
using System;

public class GameOverState2 : GameState2
{
    private GameOverView gameOverView;

    public GameOverState2(StateID stateID) : base(stateID)
    {
        gameOverView = UIController.Instance.GetView<GameOverView>();
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