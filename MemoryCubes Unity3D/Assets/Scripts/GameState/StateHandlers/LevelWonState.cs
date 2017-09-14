using System;
using UnityEngine;

public class LevelWonState : GameStateHandler
{
	public static event Action LevelWonStateStartedEvent;

	public LevelWonState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		Debug.Log("LevelWonStateStartedEvent:GameStateStarted");

		if (LevelWonStateStartedEvent != null)
		{
			LevelWonStateStartedEvent();
		}

		GameStateFinished(GameStateEventEnum.levelWonFinished);
	}
}
