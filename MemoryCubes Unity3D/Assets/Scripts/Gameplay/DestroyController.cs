using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyController : MonoBehaviour
{
    [SerializeField] private TileSelector tileSelector;

    public static event Action DestroyFinishedEvent;

	// Use this for initialization
	private void OnEnable()
    {
        PlayerCollectingCubesState.CollectingCubesStateStartedEvent += OnCollectingCubesStateStarted;
    }

    private void OnDisable()
    {
        PlayerCollectingCubesState.CollectingCubesStateStartedEvent -= OnCollectingCubesStateStarted;
    }

    private void OnCollectingCubesStateStarted()
    {
        DestroyAllSelectedTiles();
    }

    private void DestroyAllSelectedTiles()
    {
        for (int i = 0; i < tileSelector.SelectedTiles.Count; i++)
        {
            Destroyer destroyer = tileSelector.SelectedTiles[i].GetComponent<Destroyer>();

            destroyer.DestroyCube();
        }

        // TODO: When all destroy animations are done OR after a short timer, send the below event
        if (DestroyFinishedEvent != null)
        {
            DestroyFinishedEvent();
        }
    }
}
