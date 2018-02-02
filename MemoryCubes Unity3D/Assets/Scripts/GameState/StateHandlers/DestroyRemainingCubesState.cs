using System;
using UnityEngine;

public class DestroyRemainingCubesState : GameState2
{
    private CollectController collectController;

    public DestroyRemainingCubesState(StateID stateID) : base(stateID)
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