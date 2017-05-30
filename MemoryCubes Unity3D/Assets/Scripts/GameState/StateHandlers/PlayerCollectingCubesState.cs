using UnityEngine;
using System;

public class PlayerCollectingCubesState : GameStateHandler
{
    public static event Action CollectingCubesStateStartedEvent;

    public PlayerCollectingCubesState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("PlayerCollectingCubesState:GameStateStarted");

        DestroyController.DestroyFinishedEvent += OnDestroyFinished;

        CollectingCubesStateStartedEvent();
    }

    private void OnDestroyFinished()
    {
        Debug.Log("PlayerCollectingCubesState:GameStateStarted");

        DestroyController.DestroyFinishedEvent -= OnDestroyFinished;

        GameStateFinished(GameStateEventEnum.playerCollectingCubesReady);
    }
}