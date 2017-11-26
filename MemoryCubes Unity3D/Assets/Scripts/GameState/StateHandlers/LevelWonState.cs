using System;
using UnityEngine;

public class LevelWonState : GameState
{
	public static event Action LevelWonStateStartedEvent;

	public LevelWonState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

	public override void GameStateStarted()
	{
		Debug.Log("LevelWonStateStartedEvent:GameStateStarted");

		if (LevelWonStateStartedEvent != null)
		{
			LevelWonStateStartedEvent();
		}

		GameStateFinished(GameStateEvent.levelWonFinished);
	}
}
