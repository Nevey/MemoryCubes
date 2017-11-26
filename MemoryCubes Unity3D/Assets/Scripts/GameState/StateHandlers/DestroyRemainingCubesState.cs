using System;
using UnityEngine;

public class DestroyRemainingCubesState : GameState
{
    public static event Action DestroyRemainingCubesStateStartedEvent;

    public DestroyRemainingCubesState(GameStateType gameStateType) : base(gameStateType)
    {

    }

    public override void GameStateStarted()
    {
        Debug.Log("DestroyRemainingCubesState:GameStateStarted");

        CollectController.ClearAllTilesFinishedEvent += OnClearAllTilesFinished;

        if (DestroyRemainingCubesStateStartedEvent != null)
        {
            DestroyRemainingCubesStateStartedEvent();
        }
    }

    private void OnClearAllTilesFinished()
    {
        Debug.Log("DestroyRemainingCubesState:OnClearAllTilesFinished");

        CollectController.ClearAllTilesFinishedEvent -= OnClearAllTilesFinished;

        GameStateFinished(GameStateEvent.cubeCleared);
    }
}