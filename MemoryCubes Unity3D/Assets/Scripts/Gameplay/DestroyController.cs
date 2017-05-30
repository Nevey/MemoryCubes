using UnityEngine;
using System;
using System.Collections.Generic;

public class DestroyController : MonoBehaviour
{
    [SerializeField] private SelectedTiles selectedTiles;

    public static event Action DestroyFinishedEvent;

	// Use this for initialization
	void Start ()
    {
        PlayerCollectingCubesState.CollectingCubesStateStartedEvent += OnCollectingCubesStateStarted;
    }

    private void OnCollectingCubesStateStarted()
    {
        DestroyAllSelectedTiles();
    }

    private void DestroyAllSelectedTiles()
    {
        for (int i = 0; i < selectedTiles.SelectedCubesList.Count; i++)
        {
            Destroyer destroyer = selectedTiles.SelectedCubesList[i].GetComponent<Destroyer>();

            destroyer.DestroyCube();
        }

        if (DestroyFinishedEvent != null)
        {
            DestroyFinishedEvent();
        }
    }
}
