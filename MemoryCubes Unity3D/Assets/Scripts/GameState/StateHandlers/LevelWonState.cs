using System;
using UnityEngine;

public class LevelWonState2 : GameState2
{
	public LevelWonState2(StateID stateID) : base(stateID)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		GameStateFinished(GameStateEvent.levelWonFinished);
	}
}
