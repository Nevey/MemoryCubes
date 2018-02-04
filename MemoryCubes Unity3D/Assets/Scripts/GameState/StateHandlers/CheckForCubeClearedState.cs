using System;
using UnityEngine;

public class CheckForCubeClearedState2 : GameState2
{
	public CheckForCubeClearedState2(StateID stateID) : base(stateID)
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
