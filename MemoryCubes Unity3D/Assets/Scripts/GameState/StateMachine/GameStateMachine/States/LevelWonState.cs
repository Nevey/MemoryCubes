public class LevelWonState : GameState
{
    protected override void PostStart()
    {
        GameStateMachine.Instance.DoTransition<ToBuildCubeTransition>();
    }
}