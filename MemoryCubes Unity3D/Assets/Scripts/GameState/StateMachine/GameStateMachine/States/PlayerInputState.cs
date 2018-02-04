using System;

public class PlayerInputState : GameState
{
    protected override void PreStart()
    {
        CollectController.Instance.CollectFinishedEvent += OnDestroyFinished;

        TimeController.Instance.OutOfTimeEvent += OnOutOfTime;
    }

    protected override void PreFinish()
    {
        CollectController.Instance.CollectFinishedEvent -= OnDestroyFinished;

        TimeController.Instance.OutOfTimeEvent -= OnOutOfTime;
    }

    private void OnDestroyFinished()
    {
        throw new NotImplementedException();

        // TODO: To check for cube cleared transition
    }

    private void OnOutOfTime()
    {
        throw new NotImplementedException();

        // TODO: To out of time transition
    }
}