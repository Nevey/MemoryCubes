using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Base;

public class LevelController : MonoBehaviourSingleton<LevelController>
{
	private int currentLevel = 0;

	public int CurrentLevel { get { return currentLevel; } }

	public int CurrentCubeCount { get { return currentLevel + 1; } }

	private void OnEnable()
	{
		// gameStateController.GetGameState<LevelWonState>().StateStartedEvent += OnLevelWonStateStarted;

		// gameStateController.GetGameState<GameOverState>().StateFinishedEvent += OnMainMenuStateFinished;
	}

    private void OnDisable()
	{
		// gameStateController.GetGameState<LevelWonState>().StateStartedEvent -= OnLevelWonStateStarted;

		// gameStateController.GetGameState<GameOverState>().StateFinishedEvent -= OnMainMenuStateFinished;
	}

	private void OnLevelWonStateStarted(object sender, StateStartedArgs e)
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
	}

	private void ResetLevelCounter()
	{
		currentLevel = 0;
	}
}
