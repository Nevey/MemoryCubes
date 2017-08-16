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
        currentGameState = GameStateEnum.buildCube;

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
        stateHandlerList.Add(new BuildGridState(GameStateEnum.buildCube));

        stateHandlerList.Add(new SetupGameState(GameStateEnum.setupGameState));

        stateHandlerList.Add(new SelectColorTargetState(GameStateEnum.selectColorTarget));

        stateHandlerList.Add(new PlayerInputState(GameStateEnum.playerInputState));

        stateHandlerList.Add(new GameOverState(GameStateEnum.gameOverState));
    }

    private void CreateStateFlow()
    {
        // ---------- Game INIT STARTS here ---------- //

        // Move from "build cube state" to "setup game state values"
        AddStateFlow(GameStateEventEnum.cubeBuildingFinished, GameStateEnum.setupGameState);

        // Move from "setup game state values" to "select target color"
        AddStateFlow(GameStateEventEnum.setupGameStateFinished, GameStateEnum.selectColorTarget);

        // ---------- Game INIT ENDS here ---------- //



        // ---------- Game LOOP STARTS here ---------- //

        // Move from "select target color" to "player input"
        AddStateFlow(GameStateEventEnum.selectTargetColorFinished, GameStateEnum.playerInputState);

        // Move from "player input" to "select target color"
        AddStateFlow(GameStateEventEnum.playerInputStateFinished, GameStateEnum.selectColorTarget);

        // ---------- Game LOOP ENDS here ---------- //



        // ---------- Game over flow STARTS here ---------- //

        // Move from "player input" to "game over state"
        AddStateFlow(GameStateEventEnum.outOfTime, GameStateEnum.gameOverState);     

        // Move from "game over state" to "build cube state" 
        AddStateFlow(GameStateEventEnum.restartGame, GameStateEnum.buildCube);

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