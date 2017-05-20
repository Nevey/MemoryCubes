using UnityEngine;
using System;

public class PlayerCollectingCubesState : GameStateHandler
{
    public static event Action CollectingCubesStateStartedEvent;

    public PlayerCollectingCubesState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("PlayerCollectingCubesState:GameStateStarted");



        CollectingCubesStateStartedEvent();
    }

    private void WheneverThisShizzleIsDone()
    {
        Debug.Log("PlayerCollectingCubesState:WheneverThisShizzleIsDone");

        //GameStateFinished(GameStateEventEnum.cubeBuildingReady);
    }
}