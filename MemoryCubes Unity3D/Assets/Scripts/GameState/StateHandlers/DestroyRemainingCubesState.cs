using System;
using UnityEngine;

public class DestroyRemainingCubesState : GameState
{
    private CollectController collectController;

    public DestroyRemainingCubesState(GameStateID gameStateType) : base(gameStateType)
    {
        collectController = MonoBehaviour.FindObjectOfType<CollectController>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        collectController.ClearAllTilesFinishedEvent += OnClearAllTilesFinished;
    }

    private void OnClearAllTilesFinished()
    {
        collectController.ClearAllTilesFinishedEvent -= OnClearAllTilesFinished;

        GameStateFinished(GameStateEvent.cubeDestroyed);
    }
}