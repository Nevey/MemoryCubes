public class GameStateMachine : StateMachine
{
    public GameStateMachine()
    {
        AddTransition<ToMainMenuTransition, MainMenuState>();
    }
}