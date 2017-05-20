using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameStateController : MonoBehaviour
{
    private GameStateEnum currentGameState = new GameStateEnum();

    private List<GameStateHandler> stateHandlerList = new List<GameStateHandler>();

    private List<StateFlow> stateFlowList = new List<StateFlow>();

	// Use this for early initialization
	void Awake()
    {
        currentGameState = GameStateEnum.buildCube;

        CreateGameStateHandlers();

        CreateStateFlow();

        StartListeningToEvents();
    }

    // Use this for initialization
    void Start()
    {
        UpdateStateHandler();
    }

    private void CreateGameStateHandlers()
    {
        stateHandlerList.Add(new BuildCubeState(GameStateEnum.buildCube));

        stateHandlerList.Add(new SelectingCubesState(GameStateEnum.selectingCubes));
    }

    private void CreateStateFlow()
    {
        stateFlowList.Add(new StateFlow(
            GameStateEventEnum.cubeBuildingReady,
            GameStateEnum.selectingCubes));
    }

    private void StartListeningToEvents()
    {
        foreach (GameStateHandler stateHandler in stateHandlerList)
        {
            stateHandler.StateFinishedEvent += OnGameEvent;
        }
    }

    private void StopListeninToEvents()
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