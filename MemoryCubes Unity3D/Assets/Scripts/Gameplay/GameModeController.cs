using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
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
