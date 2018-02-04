using System;

public class DestroyRemainingCubesState : GameState
{
    protected override void PreStart()
    {
        CollectController.Instance.ClearAllTilesFinishedEvent += OnClearAllTilesFinished;
    }

    private void OnClearAllTilesFinished()
    {
        CollectController.Instance.ClearAllTilesFinishedEvent -= OnClearAllTilesFinished;

        // TODO: To level won state transition
    }
}