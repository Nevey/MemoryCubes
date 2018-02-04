using System;
using UnityEngine;

public class DestroyRemainingCubesState : GameState2
{
    public DestroyRemainingCubesState(StateID stateID) : base(stateID)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        CollectController.Instance.ClearAllTilesFinishedEvent += OnClearAllTilesFinished;
    }

    private void OnClearAllTilesFinished()
    {
        CollectController.Instance.ClearAllTilesFinishedEvent -= OnClearAllTilesFinished;

        GameStateFinished(GameStateEvent.cubeDestroyed);
    }
}