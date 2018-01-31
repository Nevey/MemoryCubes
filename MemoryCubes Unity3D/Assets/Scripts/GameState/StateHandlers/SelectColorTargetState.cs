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
        base.GameStateStarted();

        // TargetController.TargetUpdatedEvent += OnTargetUpdated;

        // TargetController.NoTargetFoundEvent += OnNoTargetFound;

        SelectColorTargetStateStartedEvent();
    }

    private void OnTargetUpdated()
    {
        // TargetController.TargetUpdatedEvent -= OnTargetUpdated;

        // TargetController.NoTargetFoundEvent -= OnNoTargetFound;

        GameStateFinished(GameStateEvent.selectTargetColorFinished);
    }

    private void OnNoTargetFound()
    {
        // TargetController.TargetUpdatedEvent -= OnTargetUpdated;

        // TargetController.NoTargetFoundEvent -= OnNoTargetFound;

        GameStateFinished(GameStateEvent.noTargetColorFound);
    }
}
