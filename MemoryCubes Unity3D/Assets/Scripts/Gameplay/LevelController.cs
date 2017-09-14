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

		LevelWonState.LevelWonStateStartedEvent += OnLevelWonStateStarted;

		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;
	}

	private void OnDisable()
	{
		CheckForCubeClearedState.CheckForCubeClearedStateStartedEvent -= OnCheckForCubeClearedStateStarted;

		LevelWonState.LevelWonStateStartedEvent -= OnLevelWonStateStarted;

		GameOverState.GameOverStateStartedEvent -= OnGameOverStateStarted;
	}

	private void OnCheckForCubeClearedStateStarted()
	{
		// TODO: move check for grid cleared to own class "GridClearedChecker"
		if (gridBuilder.FlattenedGridList.Count == 0)
		{
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

	private void OnLevelWonStateStarted()
	{
		IncrementLevel();
	}

	private void OnGameOverStateStarted()
	{
		ResetLevel();
	}

	private void IncrementLevel()
	{
		currentLevel++;

		Debug.Log("Level cleared, new level value: " + currentLevel);
	}

	private void ResetLevel()
	{
		currentLevel = 0;
	}
}
