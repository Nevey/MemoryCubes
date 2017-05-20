using UnityEngine;
using System;

public class SelectColorTargetState : GameStateHandler
{
    public static event Action SelectColorTargetStateStartedEvent;

    public SelectColorTargetState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SelectColorTargetState:GameStateStarted");

        TargetView.TargetColorUpdatedEvent += OnTargetColorUpdated;

        SelectColorTargetStateStartedEvent();
    }

    private void OnTargetColorUpdated()
    {
        Debug.Log("SelectColorTargetState:OnTargetColorUpdated");

        TargetView.TargetColorUpdatedEvent -= OnTargetColorUpdated;

        GameStateFinished(GameStateEventEnum.selectColorTargetReady);
    }

    private void WheneverThisShizzleIsDone()
    {
        Debug.Log("SelectColorTargetState:WheneverThisShizzleIsDone");
    }
}