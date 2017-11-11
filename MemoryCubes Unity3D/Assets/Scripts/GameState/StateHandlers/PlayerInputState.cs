using UnityEngine;
using System;

public class PlayerInputState : GameState
{
    public static event Action PlayerInputStateStartedEvent;

    public PlayerInputState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("PlayerInputState:GameStateStarted");

        CollectController.CollectFinishedEvent += OnDestroyFinished;

        TimeController.OutOfTimeEvent += OnOutOfTime;

        if (PlayerInputStateStartedEvent != null)
        {
            PlayerInputStateStartedEvent();
        }
    }

    private void OnDestroyFinished()
    {
        Debug.Log("PlayerInputState:OnCollect");

        CollectController.CollectFinishedEvent -= OnDestroyFinished;

        TimeController.OutOfTimeEvent -= OnOutOfTime;

        GameStateFinished(GameStateEvent.playerInputStateFinished);
    }

    private void OnOutOfTime()
    {
        Debug.Log("PlayerInputState:OnOutOfTime");

        CollectController.CollectFinishedEvent -= OnDestroyFinished;

        TimeController.OutOfTimeEvent -= OnOutOfTime;

        GameStateFinished(GameStateEvent.outOfTime);
    }
}