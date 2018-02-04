using System;

public class PlayerInputState : GameState
{
    protected override void PreStart()
    {
        CollectController.Instance.CollectFinishedEvent += OnCollectFinished;

        TimeController.Instance.OutOfTimeEvent += OnOutOfTime;
    }

    protected override void PreFinish()
    {
        CollectController.Instance.CollectFinishedEvent -= OnCollectFinished;

        TimeController.Instance.OutOfTimeEvent -= OnOutOfTime;
    }

    private void OnCollectFinished()
    {
        GameStateMachine.Instance.DoTransition<ToCheckForCubeClearedTransition>();
    }

    private void OnOutOfTime()
    {
        throw new NotImplementedException();

        // TODO: To out of time transition
    }
}