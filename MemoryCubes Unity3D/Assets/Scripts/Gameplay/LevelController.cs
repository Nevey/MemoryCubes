using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	[SerializeField] private GameStateController gameStateController;

	[SerializeField] private GridBuilder gridBuilder;

	private int currentLevel = 0;

	public int CurrentLevel { get { return currentLevel; } }

	public int CurrentCubeCount { get { return currentLevel + 1; } }

	public static event Action GridClearedEvent;

	public static event Action GridNotClearedEvent;

	private void OnEnable()
	{
		CheckForCubeClearedState.CheckForCubeClearedStateStartedEvent += OnCheckForCubeClearedStateStarted;

		LevelWonState.LevelWonStateStartedEvent += OnLevelWonStateStarted;

		// TODO: More like this...
		gameStateController.GetGameStateByID(GameStateType.gameOverState).StateFinishedEvent += OnMainMenuStateFinished;
	}

    private void OnDisable()
	{
		CheckForCubeClearedState.CheckForCubeClearedStateStartedEvent -= OnCheckForCubeClearedStateStarted;

		LevelWonState.LevelWonStateStartedEvent -= OnLevelWonStateStarted;

		gameStateController.GetGameStateByID(GameStateType.gameOverState).StateFinishedEvent -= OnMainMenuStateFinished;
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

	private void OnMainMenuStateFinished(object sender, StateFinishedArgs e)
    {
        ResetLevelCounter();
    }

	private void IncrementLevel()
	{
		currentLevel++;

		Debug.Log("Level cleared, new level value: " + currentLevel);
	}

	private void ResetLevelCounter()
	{
		currentLevel = 0;
	}
}
