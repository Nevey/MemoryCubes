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

        if (GameOverStateStartedEvent != null)
        {
            GameOverStateStartedEvent();
        }
    }

    // private void OnDestroyFinished()
    // {
    //     Debug.Log("GameOverState:OnCollect");

    //     GameStateFinished(GameStateEventEnum.playerInputStateFinished);
    // }
}