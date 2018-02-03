public class GameStateMachine : StateMachine
{
    public GameStateMachine()
    {
        AddTransition<ToMainMenuTransition, MainMenuState>();

        AddTransition<ToBuildCubeTransition, BuildCubeState>();

        AddTransition<ToSetupGameTransition, SetupGameState>();

        AddTransition<ToStartGameTransition, StartGameState>();
    }
}