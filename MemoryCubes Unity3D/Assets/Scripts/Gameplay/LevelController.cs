using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] private GridBuilder gridBuilder;

	private int currentLevel = 0;

	public int CurrentLevel { get { return currentLevel; } }

	public static event Action GridClearedEvent;

	public static event Action GridNotClearedEvent;

	private void OnEnable()
	{
		CheckForCubeClearedState.CheckForCubeClearedStateStartedEvent += OnCheckForCubeClearedStateStarted;
	}

	private void OnDisable()
	{
		CheckForCubeClearedState.CheckForCubeClearedStateStartedEvent -= OnCheckForCubeClearedStateStarted;
	}

	private void OnCheckForCubeClearedStateStarted()
	{
		if (gridBuilder.FlattenedGridList.Count == 0)
		{
			currentLevel++;

			Debug.Log("Level cleared, new level number: " + currentLevel);

			if (GridClearedEvent != null)
			{
				GridClearedEvent();
			}

			// TODO: update level view?
		}
		else
		{
			if (GridNotClearedEvent != null)
			{
				GridNotClearedEvent();
			}
		}
	}
}
