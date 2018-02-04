public class CheckForCubeClearedState : GameState
{
    protected override void PostFinish()
    {
        int targetColorCount = TargetController.Instance.GetTargetColorCount();

		if (targetColorCount == 1)
		{
			// TODO: To destroy remaining cubes state
		}
		else
		{
			GameStateMachine.Instance.DoTransition<ToPlayerInputTransition>();
		}
    }
}