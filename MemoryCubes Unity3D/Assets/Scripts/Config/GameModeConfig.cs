using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
	Collect,
	Combine,
}

public class GameModeConfig : MonoBehaviour
{
	[SerializeField] private GameMode[] gameModes;

	/// <summary>
	/// GameModes is a filtered list of enabled game modes
	/// </summary>
	/// <returns></returns>
	public GameMode[] GameModes { get { return gameModes; } }
}
