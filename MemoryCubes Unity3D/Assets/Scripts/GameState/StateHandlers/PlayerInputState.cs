using UnityEngine;
using System;

public class PlayerInputState : GameState
{
    public static event Action PlayerInputStateStartedEvent;

    public static event Action PlayerInputStateFinishedEvent;

    public PlayerInputState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();
        
        EnableListeners();

        DispatchPlayerInputStateStarted();
    }

    private void EnableListeners()
    {
        CollectController.CollectFinishedEvent += OnDestroyFinished;

        TimeController.OutOfTimeEvent += OnOutOfTime;
    }

    private void DisableListeners()
    {
        CollectController.CollectFinishedEvent -= OnDestroyFinished;

        TimeController.OutOfTimeEvent -= OnOutOfTime;
    }

    private void OnDestroyFinished()
    {
        DispatchPlayerInputStateFinished();

        DisableListeners();

        GameStateFinished(GameStateEvent.playerInputStateFinished);
    }

    private void OnOutOfTime()
    {
        DispatchPlayerInputStateFinished();

        DisableListeners();

        GameStateFinished(GameStateEvent.outOfTime);
    }

    private void DispatchPlayerInputStateStarted()
    {
        if (PlayerInputStateStartedEvent != null)
        {
            PlayerInputStateStartedEvent();
        }
    }

    private void DispatchPlayerInputStateFinished()
    {
        if (PlayerInputStateFinishedEvent != null)
        {
            PlayerInputStateFinishedEvent();
        }
    }
}