using System;
using UnityEngine;

public class CheckForCubeClearedState : GameState
{
	public static event Action CheckForCubeClearedStateStartedEvent;

	public CheckForCubeClearedState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		LevelController.GridClearedEvent += OnGridCleared;

		LevelController.GridNotClearedEvent += OnGridNotCleared;

		if (CheckForCubeClearedStateStartedEvent != null)
		{
			CheckForCubeClearedStateStartedEvent();
		}
	}

	private void OnGridCleared()
	{
		LevelController.GridClearedEvent -= OnGridCleared;

		LevelController.GridNotClearedEvent -= OnGridNotCleared;

		GameStateFinished(GameStateEvent.cubeCleared);
	}

	private void OnGridNotCleared()
	{
		LevelController.GridClearedEvent -= OnGridCleared;

		LevelController.GridNotClearedEvent -= OnGridNotCleared;

		GameStateFinished(GameStateEvent.cubeNotCleared);
	}
}
