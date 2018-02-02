using System;
using UnityEngine;

public class LevelWonState : GameState
{
	public LevelWonState(GameStateID gameStateEnum) : base(gameStateEnum)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		GameStateFinished(GameStateEvent.levelWonFinished);
	}
}
