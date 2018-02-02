using UnityEngine;
using System;

public class PlayerInputState : GameState2
{
    private CollectController collectController;

    private TimeController timeController;

    public PlayerInputState(StateID stateID) : base(stateID)
    {
        collectController = MonoBehaviour.FindObjectOfType<CollectController>();

        timeController = MonoBehaviour.FindObjectOfType<TimeController>();
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();
        
        EnableListeners();
    }

    private void EnableListeners()
    {
        collectController.CollectFinishedEvent += OnDestroyFinished;

        timeController.OutOfTimeEvent += OnOutOfTime;
    }

    private void DisableListeners()
    {
        collectController.CollectFinishedEvent -= OnDestroyFinished;

        timeController.OutOfTimeEvent -= OnOutOfTime;
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
