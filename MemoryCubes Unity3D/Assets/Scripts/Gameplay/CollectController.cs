using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectController : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;

    [SerializeField] private TileSelector tileSelector;

    [SerializeField] private TargetController targetController;

    [SerializeField] private TimeController timeController;

    [SerializeField] private ScoreController scoreController;

    [SerializeField] private GameModeController gameModeController;

    [SerializeField] private RoutineUtility routineUtility;

    public static event Action CollectFinishedEvent;

    public static event Action ClearAllTilesFinishedEvent;

    private void Start()
    {
        DestroyRemainingCubesState.DestroyRemainingCubesStateStartedEvent += OnDestroyRemainingCubesStateStarted;
        
        tileSelector.SelectedTilesUpdatedEvent += OnSelectedTilesUpdatedEvent;
    }

    private void OnDestroy()
    {
        DestroyRemainingCubesState.DestroyRemainingCubesStateStartedEvent -= OnDestroyRemainingCubesStateStarted;

        tileSelector.SelectedTilesUpdatedEvent -= OnSelectedTilesUpdatedEvent;
    }

    private void OnDestroyRemainingCubesStateStarted()
    {
        ClearAllTilesWithoutScore();
    }

    private void OnSelectedTilesUpdatedEvent(List<GameObject> selectedTiles)
    {
        CheckForCustomActions(selectedTiles);
    }

    private void CheckForCustomActions(List<GameObject> selectedTiles)
    {
        switch (gameModeController.CurrentGameMode)
        {
            case GameMode.Collect:
                break;
            
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

    private void RemoveAllSelectedTiles()
    {
        for (int i = 0; i < tileSelector.SelectedTiles.Count; i++)
        {
            GameObject selectedTile = tileSelector.SelectedTiles[i];
            
            if (targetController.TargetColor != selectedTile.GetComponent<TileColor>().MyColor)
            {
                // Apply penalty for each incorrectly selected tile
                timeController.ApplyTilePenalty();
                
                scoreController.ApplyPenalty();
            }
            else
            {
                // Apply bonus time for each correctly selected tile
                timeController.ApplyTileBonus();
            }

            gridBuilder.ClearTile(selectedTile);
        }

        tileSelector.ClearSelectedTiles();

        // We're ready, send collect finished event
        if (CollectFinishedEvent != null)
        {
            CollectFinishedEvent();
        }
    }

    private void ClearAllTilesWithoutScore()
    {
        gridBuilder.ClearGrid();

        routineUtility.StartWaitRoutine(2f, () =>
        {
            if (ClearAllTilesFinishedEvent != null)
            {
                ClearAllTilesFinishedEvent();
            }
        });
    }

    public void CollectSelectedTiles()
    {
        // Apply penalty if no tiles were selected
        if (tileSelector.SelectedTiles.Count == 0)
        {
            timeController.ApplyPenaltyNoSelectedTiles();
        }
        else
        {
            scoreController.AddScore(tileSelector.SelectedTiles.Count);

            RemoveAllSelectedTiles();
        }
    }
}
