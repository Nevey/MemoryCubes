using System;
using UnityEngine;

public class CheckForCubeClearedState : GameState
{
	private TargetController targetController;

	public CheckForCubeClearedState(GameStateID gameStateEnum) : base(gameStateEnum)
    {
        targetController = MonoBehaviour.FindObjectOfType<TargetController>();
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		int targetColorCount = targetController.GetTargetColorCount();

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
