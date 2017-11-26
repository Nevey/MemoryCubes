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
        Debug.Log("PlayerInputState:GameStateStarted");

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
        Debug.Log("PlayerInputState:OnCollect");

        DispatchPlayerInputStateFinished();

        DisableListeners();

        GameStateFinished(GameStateEvent.playerInputStateFinished);
    }

    private void OnOutOfTime()
    {
        Debug.Log("PlayerInputState:OnOutOfTime");

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