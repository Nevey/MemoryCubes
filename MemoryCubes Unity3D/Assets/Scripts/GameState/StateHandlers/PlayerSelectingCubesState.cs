using UnityEngine;
using System;

public class PlayerSelectingCubesState : GameStateHandler
{
    public PlayerSelectingCubesState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SelectingCubesState:GameStateStarted");

        DestroyController.DestroyFinishedEvent += OnDestroyFinished;
    }

    private void OnDestroyFinished()
    {
        Debug.Log("SelectingCubesState:OnCollect");

        DestroyController.DestroyFinishedEvent -= OnDestroyFinished;

        GameStateFinished(GameStateEventEnum.playerCollectingCubesReady);
    }
}