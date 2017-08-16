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

        TargetController.TargetUpdatedEvent += OnTargetUpdated;

        SelectColorTargetStateStartedEvent();
    }

    private void OnTargetUpdated()
    {
        Debug.Log("SelectColorTargetState:OnTargetColorUpdated");

        TargetController.TargetUpdatedEvent -= OnTargetUpdated;

        GameStateFinished(GameStateEventEnum.selectTargetColorFinished);
    }
}