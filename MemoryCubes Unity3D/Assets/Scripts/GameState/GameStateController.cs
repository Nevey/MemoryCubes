using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    private GameStateEnum currentGameState = new GameStateEnum();

    private List<GameStateHandler> stateHandlerList = new List<GameStateHandler>();

    private List<StateFlow> stateFlowList = new List<StateFlow>();

	// Use this for early initialization
	private void Awake()
    {
        currentGameState = GameStateEnum.mainMenu;

        CreateGameStateHandlers();

        CreateStateFlow();

        StartListeningToEvents();
    }

    // Use this for initialization
    private void Start()
    {
        UpdateStateHandler();
    }

    private void CreateGameStateHandlers()
    {
        stateHandlerList.Add(new MainMenuState(GameStateEnum.mainMenu));

        stateHandlerList.Add(new BuildGridState(GameStateEnum.buildCube));

        stateHandlerList.Add(new SetupGameState(GameStateEnum.setupGameState));

        stateHandlerList.Add(new SelectColorTargetState(GameStateEnum.selectColorTarget));

        stateHandlerList.Add(new PlayerInputState(GameStateEnum.playerInputState));

        stateHandlerList.Add(new CheckForCubeClearedState(GameStateEnum.checkForCubeClearedState));

        stateHandlerList.Add(new LevelWonState(GameStateEnum.levelWonState));

        stateHandlerList.Add(new GameOverState(GameStateEnum.gameOverState));
    }

    private void CreateStateFlow()
    {
        // ---------- Menu flow STARTS here ---------- //

        // Move to "build cube" state
        AddStateFlow(GameStateEventEnum.startGame, GameStateEnum.buildCube);

        // ---------- Menu flow ENDS here ---------- //



        // ---------- Grid INIT STARTS here ---------- //

        // Move from "build cube state" to "setup game state values"
        AddStateFlow(GameStateEventEnum.cubeBuildingFinished, GameStateEnum.setupGameState);

        // Move from "setup game state values" to "select target color"
        AddStateFlow(GameStateEventEnum.setupGameStateFinished, GameStateEnum.selectColorTarget);

        // ---------- Grid INIT ENDS here ---------- //



        // ---------- Gameplay LOOP STARTS here ---------- //

        // Move from "select target color" to "player input"
        AddStateFlow(GameStateEventEnum.selectTargetColorFinished, GameStateEnum.playerInputState);

        // Move from "player input" to "check for cube cleared"
        AddStateFlow(GameStateEventEnum.playerInputStateFinished, GameStateEnum.checkForCubeClearedState);

        // Move from "check for cube cleared" to "select target color"
        AddStateFlow(GameStateEventEnum.cubeNotCleared, GameStateEnum.selectColorTarget);

        // Move from "check for cube cleared" to "level won state"
        AddStateFlow(GameStateEventEnum.cubeCleared, GameStateEnum.levelWonState);

        // Move from "level won state" to "build cube state"
        AddStateFlow(GameStateEventEnum.levelWonFinished, GameStateEnum.buildCube);

        // ---------- Gameplay LOOP ENDS here ---------- //



        // ---------- Game over flow STARTS here ---------- //

        // Move from "player input" to "game over state"
        AddStateFlow(GameStateEventEnum.outOfTime, GameStateEnum.gameOverState);

        // Move from "game over state" to "build cube state"
        AddStateFlow(GameStateEventEnum.restartGame, GameStateEnum.buildCube);

        // Move from "game over state" to "main menu state"
        AddStateFlow(GameStateEventEnum.backToMenu, GameStateEnum.mainMenu);

        // ---------- Game over flow ENDS here ---------- //
    }

    private void AddStateFlow(GameStateEventEnum stateEvent, GameStateEnum state)
    {
        stateFlowList.Add(new StateFlow(stateEvent, state));
    }

    private void StartListeningToEvents()
    {
        foreach (GameStateHandler stateHandler in stateHandlerList)
        {
            stateHandler.StateFinishedEvent += OnGameEvent;
        }
    }

    private void StopListeningToEvents()
    {
        foreach (GameStateHandler stateHandler in stateHandlerList)
        {
            stateHandler.StateFinishedEvent -= OnGameEvent;
        }
    }

    private void OnGameEvent(object sender, StateFinishedArgs e)
    {
        UpdateGameEvent(e.gameStateEventEnum);

        UpdateStateHandler();
    }

    private void UpdateGameEvent(GameStateEventEnum gameEventEnum)
    {
        foreach (StateFlow stateFlow in stateFlowList)
        {
            if (gameEventEnum == stateFlow.gameStateEventEnum)
            {
                currentGameState = stateFlow.gameStateEnum;
            }
        }
    }

    private void UpdateStateHandler()
    {
        GameStateHandler gameStateHandler = GetGameStateHandlerByState(currentGameState);

        gameStateHandler.GameStateStarted();
    }

    private GameStateHandler GetGameStateHandlerByState(GameStateEnum gameStateEnum)
    {
        GameStateHandler gameStateHandler = null;

        foreach (GameStateHandler stateHandler in stateHandlerList)
        {
            if (gameStateEnum == stateHandler.gameStateEnum)
            {
                gameStateHandler = stateHandler;
            }
        }

        if (gameStateHandler == null)
        {
            Debug.LogError("Trying to handle state: GameState." + gameStateEnum.ToString() + ", but there's no GameStateHandler handling this state!");
        }

        return gameStateHandler;
    }
}