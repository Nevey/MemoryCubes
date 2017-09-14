using System;
using UnityEngine;

public class CheckForCubeClearedState : GameStateHandler
{
	public static event Action CheckForCubeClearedStateStartedEvent;

	public CheckForCubeClearedState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

	public override void GameStateStarted()
	{
		base.GameStateStarted();

		Debug.Log("CheckForCubeClearedState:GameStateStarted");

		LevelController.GridClearedEvent += OnGridCleared;

		LevelController.GridNotClearedEvent += OnGridNotCleared;

		if (CheckForCubeClearedStateStartedEvent != null)
		{
			CheckForCubeClearedStateStartedEvent();
		}
	}

	private void OnGridCleared()
	{
		Debug.Log("CheckForCubeClearedState:OnGridCleared");

		LevelController.GridClearedEvent -= OnGridCleared;

		LevelController.GridNotClearedEvent -= OnGridNotCleared;

		GameStateFinished(GameStateEventEnum.cubeCleared);
	}

	private void OnGridNotCleared()
	{
		Debug.Log("CheckForCubeClearedState:OnGridNotCleared");

		LevelController.GridClearedEvent -= OnGridCleared;

		LevelController.GridNotClearedEvent -= OnGridNotCleared;

		GameStateFinished(GameStateEventEnum.cubeNotCleared);
	}
}
