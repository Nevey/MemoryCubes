using UnityEngine;
using System;

public class PlayerInputState : GameStateHandler
{
    public static event Action PlayerInputStateStartedEvent;

    public PlayerInputState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("PlayerInputState:GameStateStarted");

        CollectController.DestroyFinishedEvent += OnDestroyFinished;

        TimeController.OutOfTimeEvent += OnOutOfTime;

        if (PlayerInputStateStartedEvent != null)
        {
            PlayerInputStateStartedEvent();
        }
    }

    private void OnDestroyFinished()
    {
        Debug.Log("PlayerInputState:OnCollect");

        CollectController.DestroyFinishedEvent -= OnDestroyFinished;

        TimeController.OutOfTimeEvent -= OnOutOfTime;

        GameStateFinished(GameStateEventEnum.playerInputStateFinished);
    }

    private void OnOutOfTime()
    {
        Debug.Log("PlayerInputState:OnOutOfTime");

        CollectController.DestroyFinishedEvent -= OnDestroyFinished;

        TimeController.OutOfTimeEvent -= OnOutOfTime;

        GameStateFinished(GameStateEventEnum.outOfTime);
    }
}