using UnityEngine;
using System;
using System.Collections.Generic;
using UnityTools.Base;

public class CollectController : MonoBehaviourSingleton<CollectController>
{
    public event Action CollectFinishedEvent;

    public event Action ClearAllTilesFinishedEvent;

    private void Start()
    {
        GameStateMachine.Instance.GetState<DestroyRemainingCubesState>().StartEvent += OnDestroyRemainingCubesStateStarted;
        
        TileSelector.Instance.SelectedTilesUpdatedEvent += OnSelectedTilesUpdatedEvent;

        TargetController.Instance.TargetUpdatedEvent += OnTargetUpdated;
    }

    private void OnDestroy()
    {
        GameStateMachine.Instance.GetState<DestroyRemainingCubesState>().StartEvent -= OnDestroyRemainingCubesStateStarted;

        TileSelector.Instance.SelectedTilesUpdatedEvent -= OnSelectedTilesUpdatedEvent;

        TargetController.Instance.TargetUpdatedEvent -= OnTargetUpdated;
    }

    private void OnDestroyRemainingCubesStateStarted()
    {
        CollectLastStandingTiles();
    }

    private void OnSelectedTilesUpdatedEvent(List<GameObject> selectedTiles)
    {
        CheckForCustomActions(selectedTiles);
    }

    private void OnTargetUpdated()
    {
        CollectPreviouslySelectedTiles();
    }

    private void CheckForCustomActions(List<GameObject> selectedTiles)
    {
        switch (GameModeController.Instance.CurrentGameMode)
        {            
            case GameMode.Combine:

                CheckForCombineCollect(selectedTiles.Count);

                break;
        }
    }

    private void CheckForCombineCollect(int selectedTilesCount)
    {
        // TODO: Add magic number to config file
        if (selectedTilesCount == 2)
        {
            CollectSelectedTiles();
        }
    }

    private void CollectTiles(List<GameObject> tileList)
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            GameObject tile = tileList[i];

            // Apply bonus time for each correctly selected tile
            TimeController.Instance.ApplyTileBonus();

            GridBuilder.Instance.ClearTile(tile);
        }

        TileAnimations.Instance.PlayCollectAnimation(tileList);

        TileSelector.Instance.ClearSelectedTiles();

        if (CollectFinishedEvent != null)
        {
            CollectFinishedEvent();
        }
    }

    private void CollectLastStandingTiles()
    {
        List<GameObject> tileAnimationList = new List<GameObject>();

        for (int x = 0; x < GridBuilder.Instance.GridSize; x++)
		{
			for (int y = 0; y < GridBuilder.Instance.GridSize; y++)
			{
				for (int z = 0; z < GridBuilder.Instance.GridSize; z++)
				{
                    GameObject tile = GridBuilder.Instance.Grid[x, y, z];

					if (tile == null)
					{
						continue;
					}

                    // TODO: Do this with the animation?
                    ScoreController.Instance.AddLastStandingTileScore();

                    GridBuilder.Instance.ClearTile(tile);

                    tileAnimationList.Add(tile);
                }
            }
        }

        TileAnimations.Instance.PlayCollectLastStandingTilesAnimation(tileAnimationList, ClearAllTilesFinishedEvent);
    }

    public void CollectSelectedTiles()
    {
        // Apply penalty if no tiles were selected
        if (TileSelector.Instance.SelectedTiles.Count == 0)
        {
            TimeController.Instance.ApplyPenaltyNoSelectedTiles();
        }
        else
        {
            ScoreController.Instance.AddBulkScore(TileSelector.Instance.SelectedTiles.Count);

            CollectTiles(TileSelector.Instance.SelectedTiles);
        }
    }

    public void CollectPreviouslySelectedTiles()
    {
        // TODO: Clean this up, think about adding an extra state for when "Selecting first target" per level
        // At start of the game, no tiles were selected
        if (TileSelector.Instance.PreviouslySelectedTiles.Count == 0)
        {
            return;
        }

        if (TileSelector.Instance.PreviouslySelectedTiles.Count == 1)
        {
            // Only apply penalties if grid size is big enough
            if (GridBuilder.Instance.GridSize > 2)
            {
                ScoreController.Instance.ApplyPenalty();

                TimeController.Instance.ApplyTilePenalty();
            }
        }
        else
        {
            ScoreController.Instance.AddBulkScore(TileSelector.Instance.PreviouslySelectedTiles.Count);
        }

        CollectTiles(TileSelector.Instance.PreviouslySelectedTiles);
    }
}