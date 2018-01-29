using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// TODO: Make this class something to inherit from, so we can create state flows seperately from each other
public class GameStateController : MonoBehaviour
{
    private GameStateType currentGameState = new GameStateType();

    private List<GameState> gameStateList = new List<GameState>();

    private List<StateTransition> transitionList = new List<StateTransition>();

	// Use this for early initialization
	private void Awake()
    {
        // Set initial state
        currentGameState = GameStateType.mainMenu;

        CreateGameStateHandlers();

        CreateStateFlow();

        StartListeningToEvents();
    }

    // Use this for initialization
    private void Start()
    {
        TransitionToNextState();
    }

    private void CreateGameStateHandlers()
    {
        gameStateList.Add(new MainMenuState(GameStateType.mainMenu));

        gameStateList.Add(new BuildGridState(GameStateType.buildCube));

        gameStateList.Add(new SetupGameState(GameStateType.setupGameState));

        gameStateList.Add(new StartGameState(GameStateType.startGameState));

        gameStateList.Add(new SelectColorTargetState(GameStateType.selectColorTarget));

        gameStateList.Add(new DestroyRemainingCubesState(GameStateType.destroyRemainingCubesState));

        gameStateList.Add(new PlayerInputState(GameStateType.playerInputState));

        gameStateList.Add(new CheckForCubeClearedState(GameStateType.checkForCubeClearedState));

        gameStateList.Add(new LevelWonState(GameStateType.levelWonState));

        gameStateList.Add(new GameOverState(GameStateType.gameOverState));
    }

    private void CreateStateFlow()
    {
        // ---------- Menu flow STARTS here ---------- //

        // Move to "build cube" state
        AddTransition(GameStateEvent.startGame, GameStateType.buildCube);

        // ---------- Menu flow ENDS here ---------- //



        // ---------- Grid INIT STARTS here ---------- //

        // Move from "build cube state" to "setup game state values"
        AddTransition(GameStateEvent.cubeBuildingFinished, GameStateType.setupGameState);

        // Move from "setup game state" to "start game state"
        AddTransition(GameStateEvent.setupGameStateFinished, GameStateType.startGameState);

        // Move from "start game state" to "select target color"
        AddTransition(GameStateEvent.startGameStateFinished, GameStateType.selectColorTarget);

        // ---------- Grid INIT ENDS here ---------- //



        // ---------- Gameplay LOOP STARTS here ---------- //

        // Move from "select target color" to "player input"
        AddTransition(GameStateEvent.selectTargetColorFinished, GameStateType.playerInputState);

        // Move from "select target color" to "destroy cube"
        AddTransition(GameStateEvent.noTargetColorFound, GameStateType.destroyRemainingCubesState);

        // Move from "destroy cube" to "level won"
        AddTransition(GameStateEvent.cubeDestroyed, GameStateType.levelWonState);

        // Move from "player input" to "check for cube cleared"
        AddTransition(GameStateEvent.playerInputStateFinished, GameStateType.checkForCubeClearedState);

        // Move from "check for cube cleared" to "select target color"
        AddTransition(GameStateEvent.cubeNotCleared, GameStateType.selectColorTarget);

        // Move from "check for cube cleared" to "level won state"
        AddTransition(GameStateEvent.cubeCleared, GameStateType.levelWonState);

        // Move from "level won state" to "build cube state"
        AddTransition(GameStateEvent.levelWonFinished, GameStateType.buildCube);

        // ---------- Gameplay LOOP ENDS here ---------- //



        // ---------- Game over flow STARTS here ---------- //

        // Move from "player input" to "game over state"
        AddTransition(GameStateEvent.outOfTime, GameStateType.gameOverState);

        // Move from "game over state" to "build cube state"
        AddTransition(GameStateEvent.restartGame, GameStateType.buildCube);

        // Move from "game over state" to "main menu state"
        AddTransition(GameStateEvent.backToMenu, GameStateType.mainMenu);

        // ---------- Game over flow ENDS here ---------- //
    }

    private void AddTransition(GameStateEvent stateEvent, GameStateType state)
    {
        transitionList.Add(new StateTransition(stateEvent, state));
    }

    private void StartListeningToEvents()
    {
        foreach (GameState stateHandler in gameStateList)
        {
            stateHandler.StateFinishedEvent += OnStateFinished;
        }
    }

    private void StopListeningToEvents()
    {
        foreach (GameState stateHandler in gameStateList)
        {
            stateHandler.StateFinishedEvent -= OnStateFinished;
        }
    }

    private void OnStateFinished(object sender, StateFinishedArgs e)
    {
        UpdateGameEvent(e.gameStateEventEnum);

        TransitionToNextState();
    }

    private void UpdateGameEvent(GameStateEvent gameEventEnum)
    {
        foreach (StateTransition stateFlow in transitionList)
        {
            if (gameEventEnum == stateFlow.gameStateEventEnum)
            {
                currentGameState = stateFlow.gameStateEnum;
            }
        }
    }

    private void TransitionToNextState()
    {
        GameState gameState = GetGameStateByEnum(currentGameState);

        gameState.GameStateStarted();
    }

    private GameState GetGameStateByEnum(GameStateType gameStateEnum)
    {
        GameState gameState = null;

        foreach (GameState gs in gameStateList)
        {
            if (gameStateEnum == gs.gameStateType)
            {
                if (gameState != null)
                {
                    Debug.LogError("Multiple GameStates present of type: " + gs.gameStateType);
                }

                gameState = gs;
            }
        }

        if (gameState == null)
        {
            Debug.LogError("No GameState present with type: " + gameStateEnum.ToString());
        }

        return gameState;
    }

    // TODO: Use generic type in stead of ID/Type
    public GameState GetGameStateByID(GameStateType gameStateType)
    {
        for (int i = 0; i < gameStateList.Count; i++)
        {
            GameState gameState = gameStateList[i];

            if (gameState.gameStateType == gameStateType)
            {
                return gameState;
            }
        }

        Debug.LogError("Unable to find Game State with ID: " + gameStateType);

        return null;
    }
}