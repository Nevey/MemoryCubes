using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Base;

public class GameModeController : MonoBehaviourSingleton<GameModeController>
{
	private GameMode currentGameMode;

	public GameMode CurrentGameMode { get { return currentGameMode; } }

	private void OnEnable()
	{

	}

	private void OnDisable()
	{

	}

	public void SetGameMode(GameMode gameMode)
	{
		currentGameMode = gameMode;
	}
}
