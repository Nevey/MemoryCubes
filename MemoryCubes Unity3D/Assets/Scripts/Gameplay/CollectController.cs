using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectController : MonoBehaviour
{
    [SerializeField] private Builder builder;

    [SerializeField] private TileSelector tileSelector;

    [SerializeField] private TargetController targetController;

    [SerializeField] private TimeController targetTime;

    [SerializeField] private ScoreController scoreController;

    [SerializeField] private GameOverView gameOverView;

    public static event Action DestroyFinishedEvent;

    private void OnEnable()
    {
        gameOverView.GameOverShowFinishedEvent += OnGameOverShowFinished;
    }

    private void OnDisable()
    {
        gameOverView.GameOverShowFinishedEvent -= OnGameOverShowFinished;
    }

    private void OnGameOverShowFinished()
    {
        DestroyAllTiles();
    }

    private void RemoveAllSelectedTiles()
    {
        for (int i = 0; i < tileSelector.SelectedTiles.Count; i++)
        {
            GameObject selectedTile = tileSelector.SelectedTiles[i];

            if (targetController.TargetColor != selectedTile.GetComponent<TileColor>().MyColor)
            {
                targetTime.ApplyTilePenalty();
                
                scoreController.ApplyPenalty();
            }
            else
            {
                targetTime.ApplyTileBonus();
            }

            Destroyer destroyer = selectedTile.GetComponent<Destroyer>();

            destroyer.DestroyCube();
        }

        tileSelector.ClearSelectedTiles();

        // TODO: When all destroy animations are done OR after a short timer, send the below event
        if (DestroyFinishedEvent != null)
        {
            DestroyFinishedEvent();
        }
    }

    private void DestroyAllTiles()
    {
        for (int i = 0; i < builder.FlattenedGridList.Count; i++)
        {
            GameObject tile = builder.FlattenedGridList[i];

            // Tile could already be destroyed
            if (tile == null)
            {
                continue;
            }

            Destroyer destroyer = tile.GetComponent<Destroyer>();

            destroyer.DestroyCube();
        }

        builder.ClearGrid();
    }

    public void CollectCubes()
    {
        // Apply penalty if no tiles were selected
        if (tileSelector.SelectedTiles.Count == 0)
        {
            targetTime.ApplyPenaltyNoSelectedTiles();
        }
        else
        {
            scoreController.AddScore(tileSelector.SelectedTiles.Count);

            RemoveAllSelectedTiles();
        }
    }
}
