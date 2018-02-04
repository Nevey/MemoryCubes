public class SetupGameState : GameState
{
    protected override void PostStart()
    {
        // Maybe instead of immediately moving on, we can wait for some animations and then do transition?
        GameStateMachine.Instance.DoTransition<ToStartGameTransition>();
    }
}