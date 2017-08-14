using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyController : MonoBehaviour
{
    [SerializeField] private TileSelector tileSelector;

    [SerializeField] private TargetSelector targetSelector;

    [SerializeField] private TargetTime targetTime;

    [SerializeField] private Builder builder;

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

    private void DestroyAllSelectedTiles()
    {
        // Apply penalty if no tiles were selected
        if (tileSelector.SelectedTiles.Count == 0)
        {
            targetTime.ApplyPenalty();
        }

        for (int i = 0; i < tileSelector.SelectedTiles.Count; i++)
        {
            GameObject selectedTile = tileSelector.SelectedTiles[i];

            if (targetSelector.TargetColor != selectedTile.GetComponent<TileColor>().MyColor)
            {
                targetTime.ApplyPenalty();
                // TAKE POINTS!
            }
            else
            {
                targetTime.ApplyBonus();
                // Give points!!!
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
        DestroyAllSelectedTiles();
    }
}
