using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeCorresponder : MonoBehaviour
{
	[SerializeField] private GameMode gameMode;

	public GameMode CorrespindingGameMode { get { return gameMode; } }

	public bool IsGameMode(GameMode gameMode)
	{
		return this.gameMode == gameMode;
	}
}
