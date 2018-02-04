public class CheckForCubeClearedState : GameState
{
    protected override void PostStart()
    {
        int targetColorCount = TargetController.Instance.GetTargetColorCount();

		if (targetColorCount == 1)
		{
			GameStateMachine.Instance.DoTransition<ToDestroyRemainingCubesTransition>();
		}
		else
		{
			GameStateMachine.Instance.DoTransition<ToPlayerInputTransition>();
		}
    }
}