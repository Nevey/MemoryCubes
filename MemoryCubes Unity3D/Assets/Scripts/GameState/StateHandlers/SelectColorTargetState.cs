using UnityEngine;
using System;

public class SelectColorTargetState : GameState
{
    public static event Action SelectColorTargetStateStartedEvent;

    public SelectColorTargetState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        Debug.Log("SelectColorTargetState:GameStateStarted");

        TargetController.TargetUpdatedEvent += OnTargetUpdated;

        TargetController.NoTargetFoundEvent += OnNoTargetFound;

        SelectColorTargetStateStartedEvent();
    }

    private void OnTargetUpdated()
    {
        Debug.Log("SelectColorTargetState:OnTargetColorUpdated");

        TargetController.TargetUpdatedEvent -= OnTargetUpdated;

        TargetController.NoTargetFoundEvent -= OnNoTargetFound;

        GameStateFinished(GameStateEvent.selectTargetColorFinished);
    }

    private void OnNoTargetFound()
    {
        Debug.Log("SelectColorTargetState:OnNoTargetFound");

        TargetController.TargetUpdatedEvent -= OnTargetUpdated;

        TargetController.NoTargetFoundEvent -= OnNoTargetFound;

        GameStateFinished(GameStateEvent.noTargetColorFound);
    }
}