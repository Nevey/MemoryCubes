using System;
using UnityEngine;

public class DestroyRemainingCubesState2 : GameState2
{
    public DestroyRemainingCubesState2(StateID stateID) : base(stateID)
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