public class SetupGameState : GameState
{
    public override void Start()
    {
        base.Start();

        // Maybe instead of immediately moving on, we can wait for some animations and then do transition?
        GameStateMachine.Instance.DoTransition<ToStartGameTransition>();
    }
}