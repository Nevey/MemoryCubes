using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyController : MonoBehaviour
{
    [SerializeField] private TileSelector tileSelector;

    [SerializeField] private TargetSelector targetSelector;

    [SerializeField] private TargetTime targetTime;

    public static event Action DestroyFinishedEvent;

    private void DestroyAllSelectedTiles()
    {
        for (int i = 0; i < tileSelector.SelectedTiles.Count; i++)
        {
            GameObject selectedTile = tileSelector.SelectedTiles[i];

            if (targetSelector.TargetColor != selectedTile.GetComponent<TileColor>().MyColor)
            {
                targetTime.ApplyPenalty();
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

    public void CollectCubes()
    {
        DestroyAllSelectedTiles();
    }
}
