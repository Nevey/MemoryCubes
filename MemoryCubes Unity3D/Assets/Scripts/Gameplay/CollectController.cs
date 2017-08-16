using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectController : MonoBehaviour
{
    [SerializeField] private GridBuilder gridBuilder;

    [SerializeField] private TileSelector tileSelector;

    [SerializeField] private TargetController targetController;

    [SerializeField] private TimeController targetTime;

    [SerializeField] private ScoreController scoreController;

    public static event Action CollectFinishedEvent;

    private void RemoveAllSelectedTiles()
    {
        for (int i = 0; i < tileSelector.SelectedTiles.Count; i++)
        {
            GameObject selectedTile = tileSelector.SelectedTiles[i];
            
            if (targetController.TargetColor != selectedTile.GetComponent<TileColor>().MyColor)
            {
                // Apply penalty for each incorrectly selected tile

                targetTime.ApplyTilePenalty();
                
                scoreController.ApplyPenalty();
            }
            else
            {
                // Apply bonus time for each correctly selected tile
                
                targetTime.ApplyTileBonus();
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

    public void CollectSelectedTiles()
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
