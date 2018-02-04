public class GameStateMachine : StateMachine
{
    public GameStateMachine()
    {
        // --- Menu

        AddTransition<ToMainMenuTransition, MainMenuState>();

        // --- Setup level

        AddTransition<ToBuildCubeTransition, BuildCubeState>();

        AddTransition<ToSetupGameTransition, SetupGameState>();

        AddTransition<ToStartGameTransition, StartGameState>();

        // --- Gameplay loop

        AddTransition<ToPlayerInputTransition, PlayerInputState>();

        AddTransition<ToCheckForCubeClearedTransition, CheckForCubeClearedState>();

        AddTransition<ToDestroyRemainingCubesTransition, DestroyRemainingCubesState>();

        AddTransition<ToLevelWonTrainsition, LevelWonState>();
    }
}