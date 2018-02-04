using UnityEngine;
using System;

public class PlayerInputState : GameState2
{
    public PlayerInputState(StateID stateID) : base(stateID)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();
        
        EnableListeners();
    }

    private void EnableListeners()
    {
        CollectController.Instance.CollectFinishedEvent += OnDestroyFinished;

        TimeController.Instance.OutOfTimeEvent += OnOutOfTime;
    }

    private void DisableListeners()
    {
        CollectController.Instance.CollectFinishedEvent -= OnDestroyFinished;

        TimeController.Instance.OutOfTimeEvent -= OnOutOfTime;
    }

    private void OnDestroyFinished()
    {
        DisableListeners();

        GameStateFinished(GameStateEvent.playerInputStateFinished);
    }

    private void OnOutOfTime()
    {
        DisableListeners();

        GameStateFinished(GameStateEvent.outOfTime);
    }
}
