public class StartGameState : GameState
{
    protected override void PostStart()
    {
        GameStateMachine.Instance.DoTransition<ToPlayerInputTransition>();
    }
}