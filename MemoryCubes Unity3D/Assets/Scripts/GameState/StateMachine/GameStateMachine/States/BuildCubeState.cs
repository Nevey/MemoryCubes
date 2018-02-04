using System;

public class BuildCubeState : GameState
{
    protected override void PreStart()
    {
        GridBuilder.Instance.BuilderReadyEvent += OnBuilderReady;
    }

    private void OnBuilderReady()
    {
        GridBuilder.Instance.BuilderReadyEvent -= OnBuilderReady;

        GameStateMachine.Instance.DoTransition<ToSetupGameTransition>();
    }
}