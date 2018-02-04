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
		GameStateMachine.Instance.GetState<LevelWonState>().StartEvent += OnLevelWonStateStarted;

		GameStateMachine.Instance.GetState<GameOverState>().StartEvent += OnGameOverStateStarted;
	}

    private void OnDisable()
	{
		GameStateMachine.Instance.GetState<LevelWonState>().StartEvent -= OnLevelWonStateStarted;

		GameStateMachine.Instance.GetState<GameOverState>().StartEvent -= OnGameOverStateStarted;
	}

	private void OnLevelWonStateStarted()
	{
		IncrementLevel();
	}

	private void OnGameOverStateStarted()
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
