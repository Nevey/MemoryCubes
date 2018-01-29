using UnityEngine;
using System;

public class GameOverState : GameState
{
    public static event Action GameOverStateStartedEvent;

    public GameOverState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        GameOverView.GameOverHideFinishedEvent += OnGameOverHideFinished;

        if (GameOverStateStartedEvent != null)
        {
            GameOverStateStartedEvent();
        }
    }

    private void OnGameOverHideFinished()
    {
        GameOverView.GameOverHideFinishedEvent -= OnGameOverHideFinished;

        GameStateFinished(GameStateEvent.backToMenu);
    }
}