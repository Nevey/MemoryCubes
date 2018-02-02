using System;
using UnityEngine;

public class LevelWonState : GameState2
{
	public LevelWonState(StateID stateID) : base(stateID)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		GameStateFinished(GameStateEvent.levelWonFinished);
	}
}
