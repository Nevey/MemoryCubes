using System;

public class BuildCubeState : GameState
{
    protected override void PreStart()
    {
        GridBuilder.Instance.BuilderReadyEvent += OnBuilderReady;
    }

    protected override void PreFinish()
    {
        GridBuilder.Instance.BuilderReadyEvent -= OnBuilderReady;
    }

    private void OnBuilderReady()
    {
        GameStateMachine.Instance.DoTransition<ToSetupGameTransition>();
    }
}