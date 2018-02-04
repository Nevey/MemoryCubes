using System;
using UnityEngine;

public class CheckForCubeClearedState : GameState2
{
	public CheckForCubeClearedState(StateID stateID) : base(stateID)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		int targetColorCount = TargetController.Instance.GetTargetColorCount();

		if (targetColorCount == 1)
		{
			GameStateFinished(GameStateEvent.cubeCleared);
		}
		else
		{
			GameStateFinished(GameStateEvent.cubeNotCleared);
		}
	}
}
