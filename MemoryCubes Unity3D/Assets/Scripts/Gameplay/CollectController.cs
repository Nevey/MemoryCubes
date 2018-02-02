using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectController : MonoBehaviour
{
    [SerializeField] private GameStateController gameStateController;

    [SerializeField] private GridBuilder gridBuilder;

    [SerializeField] private TileSelector tileSelector;

    [SerializeField] private TargetController targetController;

    [SerializeField] private TimeController timeController;

    [SerializeField] private ScoreController scoreController;

    [SerializeField] private GameModeController gameModeController;

    [SerializeField] private RoutineUtility routineUtility;

    public event Action CollectFinishedEvent;

    public event Action ClearAllTilesFinishedEvent;

    private void Start()
    {
        gameStateController.GetGameState<DestroyRemainingCubesState>().StateStartedEvent += OnDestroyRemainingCubesStateStarted;
        
        tileSelector.SelectedTilesUpdatedEvent += OnSelectedTilesUpdatedEvent;

        targetController.TargetUpdatedEvent += OnTargetUpdated;
    }

    private void OnDestroy()
    {
        gameStateController.GetGameState<DestroyRemainingCubesState>().StateStartedEvent -= OnDestroyRemainingCubesStateStarted;

        tileSelector.SelectedTilesUpdatedEvent -= OnSelectedTilesUpdatedEvent;

        targetController.TargetUpdatedEvent -= OnTargetUpdated;
    }

    private void OnDestroyRemainingCubesStateStarted(object sender, StateStartedArgs e)
    {
        ClearLastStandingTilesWithDelay();
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
        switch (gameModeController.CurrentGameMode)
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

    private void RemoveAllSelectedTiles(List<GameObject> tileList)
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            GameObject selectedTile = tileList[i];

            // Apply bonus time for each correctly selected tile
            timeController.ApplyTileBonus();

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

        routineUtility.StartWaitTimeRoutine(2f, () =>
        {
            if (ClearAllTilesFinishedEvent != null)
            {
                ClearAllTilesFinishedEvent();
            }
        });
    }

    private void ClearLastStandingTilesWithDelay()
    {
        gridBuilder.ClearGridDelayed(0.1f, (bool isFinished) =>
        {
            scoreController.AddLastStandingTileScore();

            if (isFinished)
            {
                routineUtility.StartWaitTimeRoutine(2f, () =>
                {
                    if (ClearAllTilesFinishedEvent != null)
                    {
                        ClearAllTilesFinishedEvent();
                    }
                });
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
            scoreController.AddBulkScore(tileSelector.SelectedTiles.Count);

            RemoveAllSelectedTiles(tileSelector.SelectedTiles);
        }
    }

    public void CollectPreviouslySelectedTiles()
    {
        scoreController.AddBulkScore(tileSelector.PreviouslySelectedTiles.Count);

        RemoveAllSelectedTiles(tileSelector.PreviouslySelectedTiles);
    }
}
