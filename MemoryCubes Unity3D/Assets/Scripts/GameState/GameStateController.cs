using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    private GameStateType currentGameState = new GameStateType();

    private List<GameState> gameStateList = new List<GameState>();

    private List<StateFlow> stateFlowList = new List<StateFlow>();

	// Use this for early initialization
	private void Awake()
    {
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

        gameStateList.Add(new PlayerInputState(GameStateType.playerInputState));

        gameStateList.Add(new CheckForCubeClearedState(GameStateType.checkForCubeClearedState));

        gameStateList.Add(new LevelWonState(GameStateType.levelWonState));

        gameStateList.Add(new GameOverState(GameStateType.gameOverState));
    }

    private void CreateStateFlow()
    {
        // ---------- Menu flow STARTS here ---------- //

        // Move to "build cube" state
        AddStateFlow(GameStateEvent.startGame, GameStateType.buildCube);

        // ---------- Menu flow ENDS here ---------- //



        // ---------- Grid INIT STARTS here ---------- //

        // Move from "build cube state" to "setup game state values"
        AddStateFlow(GameStateEvent.cubeBuildingFinished, GameStateType.setupGameState);

        // Move from "setup game state" to "start game state"
        AddStateFlow(GameStateEvent.setupGameStateFinished, GameStateType.startGameState);

        // Move from "start game state" to "select target color"
        AddStateFlow(GameStateEvent.startGameStateFinished, GameStateType.selectColorTarget);

        // ---------- Grid INIT ENDS here ---------- //



        // ---------- Gameplay LOOP STARTS here ---------- //

        // Move from "select target color" to "player input"
        AddStateFlow(GameStateEvent.selectTargetColorFinished, GameStateType.playerInputState);

        // Move from "player input" to "check for cube cleared"
        AddStateFlow(GameStateEvent.playerInputStateFinished, GameStateType.checkForCubeClearedState);

        // Move from "check for cube cleared" to "select target color"
        AddStateFlow(GameStateEvent.cubeNotCleared, GameStateType.selectColorTarget);

        // Move from "check for cube cleared" to "level won state"
        AddStateFlow(GameStateEvent.cubeCleared, GameStateType.levelWonState);

        // Move from "level won state" to "build cube state"
        AddStateFlow(GameStateEvent.levelWonFinished, GameStateType.buildCube);

        // ---------- Gameplay LOOP ENDS here ---------- //



        // ---------- Game over flow STARTS here ---------- //

        // Move from "player input" to "game over state"
        AddStateFlow(GameStateEvent.outOfTime, GameStateType.gameOverState);

        // Move from "game over state" to "build cube state"
        AddStateFlow(GameStateEvent.restartGame, GameStateType.buildCube);

        // Move from "game over state" to "main menu state"
        AddStateFlow(GameStateEvent.backToMenu, GameStateType.mainMenu);

        // ---------- Game over flow ENDS here ---------- //
    }

    private void AddStateFlow(GameStateEvent stateEvent, GameStateType state)
    {
        stateFlowList.Add(new StateFlow(stateEvent, state));
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
        foreach (StateFlow stateFlow in stateFlowList)
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
}