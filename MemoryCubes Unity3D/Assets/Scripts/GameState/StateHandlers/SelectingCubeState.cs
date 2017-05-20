using UnityEngine;
using System;

public class SelectingCubesState : GameStateHandler
{
    public static event Action SelectingCubesStateStartedEvent;

    public SelectingCubesState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SelectingCubesState:GameStateStarted");

        SelectingCubesStateStartedEvent();
    }

    private void WheneverThisShizzleIsDone()
    {
        Debug.Log("SelectingCubesState:WheneverThisShizzleIsDone");

        //GameStateFinished(GameStateEventEnum.cubeBuildingReady);
    }
}