using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

// TODO: Make this class something to inherit from, so we can create state flows seperately from each other
public class GameStateController : MonoBehaviour
{
    private GameStateID currentGameState = new GameStateID();

    private List<GameState> gameStateList = new List<GameState>();

    private List<StateTransition> transitionList = new List<StateTransition>();

	// Use this for early initialization
	private void Awake()
    {
        // Set initial state
        currentGameState = GameStateID.mainMenu;

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
        gameStateList.Add(new MainMenuState(GameStateID.mainMenu));

        gameStateList.Add(new BuildGridState(GameStateID.buildCube));

        gameStateList.Add(new SetupGameState(GameStateID.setupGameState));

        gameStateList.Add(new StartGameState(GameStateID.startGameState));

        gameStateList.Add(new DestroyRemainingCubesState(GameStateID.destroyRemainingCubesState));

        gameStateList.Add(new PlayerInputState(GameStateID.playerInputState));

        gameStateList.Add(new CheckForCubeClearedState(GameStateID.checkForCubeClearedState));

        gameStateList.Add(new LevelWonState(GameStateID.levelWonState));

        gameStateList.Add(new GameOverState(GameStateID.gameOverState));
    }

    private void CreateStateFlow()
    {
        // ---------- Menu flow STARTS here ---------- //

        // Move to "build cube" state
        AddTransition(GameStateEvent.startGame, GameStateID.buildCube);

        // ---------- Menu flow ENDS here ---------- //



        // ---------- Grid INIT STARTS here ---------- //

        // Move from "build cube state" to "setup game state values"
        AddTransition(GameStateEvent.cubeBuildingFinished, GameStateID.setupGameState);

        // Move from "setup game state" to "start game state"
        AddTransition(GameStateEvent.setupGameStateFinished, GameStateID.startGameState);

        // Move from "start game state" to "select target color"
        // AddTransition(GameStateEvent.startGameStateFinished, GameStateType.selectColorTarget);

        // Move from "start game state" to "player input state"
        AddTransition(GameStateEvent.startGameStateFinished, GameStateID.playerInputState);

        // ---------- Grid INIT ENDS here ---------- //



        // ---------- Gameplay LOOP STARTS here ---------- //

        // Move from "destroy cube" to "level won"
        AddTransition(GameStateEvent.cubeDestroyed, GameStateID.levelWonState);

        // Move from "player input" to "check for cube cleared"
        AddTransition(GameStateEvent.playerInputStateFinished, GameStateID.checkForCubeClearedState);

        // Move from "check for cube cleared" to "select target color"
        AddTransition(GameStateEvent.cubeNotCleared, GameStateID.playerInputState);

        // Move from "check for cube cleared" to "level won state"
        AddTransition(GameStateEvent.cubeCleared, GameStateID.destroyRemainingCubesState);

        // Move from "level won state" to "build cube state"
        AddTransition(GameStateEvent.levelWonFinished, GameStateID.buildCube);

        // ---------- Gameplay LOOP ENDS here ---------- //



        // ---------- Game over flow STARTS here ---------- //

        // Move from "player input" to "game over state"
        AddTransition(GameStateEvent.outOfTime, GameStateID.gameOverState);

        // Move from "game over state" to "build cube state"
        AddTransition(GameStateEvent.restartGame, GameStateID.buildCube);

        // Move from "game over state" to "main menu state"
        AddTransition(GameStateEvent.backToMenu, GameStateID.mainMenu);

        // ---------- Game over flow ENDS here ---------- //
    }

    private void AddTransition(GameStateEvent stateEvent, GameStateID state)
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

    private GameState GetGameStateByEnum(GameStateID gameStateEnum)
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

    public T GetGameState<T>() where T : GameState
    {
        for (int i = 0; i < gameStateList.Count; i++)
        {
            Type type = gameStateList[i].GetType();

            if (type == typeof(T))
			{
				return gameStateList[i] as T;
			}
        }

        Debug.LogError("Unable to find Game State of type: " + typeof(T).Name);

        return null;
    }
}
