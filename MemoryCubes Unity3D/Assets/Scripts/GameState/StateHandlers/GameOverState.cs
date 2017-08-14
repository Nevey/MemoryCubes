using UnityEngine;
using System;

public class GameOverState : GameStateHandler
{
    public static event Action GameOverStateStartedEvent;

    public GameOverState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("GameOverState:GameStateStarted");

        GameOverView.GameOverHideFinishedEvent += OnGameOverHideFinished;

        if (GameOverStateStartedEvent != null)
        {
            GameOverStateStartedEvent();
        }
    }

    private void OnGameOverHideFinished()
    {
        Debug.Log("GameOverState:OnGameOverHideFinished");

        GameStateFinished(GameStateEventEnum.restartGame);
    }
}