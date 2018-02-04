using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

// TODO: Make this class something to inherit from, so we can create state flows seperately from each other
public class GameStateController : MonoBehaviour
{
    private StateID currentGameState = new StateID();

    private List<GameState2> gameStateList = new List<GameState2>();

    private List<StateTransition2> transitionList = new List<StateTransition2>();

	// Use this for early initialization
	private void Awake()
    {
        // Set initial state
        currentGameState = StateID.mainMenu;

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
        gameStateList.Add(new MainMenuState2(StateID.mainMenu));

        gameStateList.Add(new BuildGridState(StateID.buildCube));

        gameStateList.Add(new SetupGameState2(StateID.setupGameState));

        gameStateList.Add(new StartGameState2(StateID.startGameState));

        gameStateList.Add(new DestroyRemainingCubesState(StateID.destroyRemainingCubesState));

        gameStateList.Add(new PlayerInputState2(StateID.playerInputState));

        gameStateList.Add(new CheckForCubeClearedState(StateID.checkForCubeClearedState));

        gameStateList.Add(new LevelWonState(StateID.levelWonState));

        gameStateList.Add(new GameOverState(StateID.gameOverState));
    }

    private void CreateStateFlow()
    {
        // ---------- Menu flow STARTS here ---------- //

        // Move to "build cube" state
        AddTransition(GameStateEvent.startGame, StateID.buildCube);

        // ---------- Menu flow ENDS here ---------- //



        // ---------- Grid INIT STARTS here ---------- //

        // Move from "build cube state" to "setup game state values"
        AddTransition(GameStateEvent.cubeBuildingFinished, StateID.setupGameState);

        // Move from "setup game state" to "start game state"
        AddTransition(GameStateEvent.setupGameStateFinished, StateID.startGameState);

        // Move from "start game state" to "select target color"
        // AddTransition(GameStateEvent.startGameStateFinished, GameStateType.selectColorTarget);

        // Move from "start game state" to "player input state"
        AddTransition(GameStateEvent.startGameStateFinished, StateID.playerInputState);

        // ---------- Grid INIT ENDS here ---------- //



        // ---------- Gameplay LOOP STARTS here ---------- //

        // Move from "destroy cube" to "level won"
        AddTransition(GameStateEvent.cubeDestroyed, StateID.levelWonState);

        // Move from "player input" to "check for cube cleared"
        AddTransition(GameStateEvent.playerInputStateFinished, StateID.checkForCubeClearedState);

        // Move from "check for cube cleared" to "select target color"
        AddTransition(GameStateEvent.cubeNotCleared, StateID.playerInputState);

        // Move from "check for cube cleared" to "level won state"
        AddTransition(GameStateEvent.cubeCleared, StateID.destroyRemainingCubesState);

        // Move from "level won state" to "build cube state"
        AddTransition(GameStateEvent.levelWonFinished, StateID.buildCube);

        // ---------- Gameplay LOOP ENDS here ---------- //



        // ---------- Game over flow STARTS here ---------- //

        // Move from "player input" to "game over state"
        AddTransition(GameStateEvent.outOfTime, StateID.gameOverState);

        // Move from "game over state" to "build cube state"
        AddTransition(GameStateEvent.restartGame, StateID.buildCube);

        // Move from "game over state" to "main menu state"
        AddTransition(GameStateEvent.backToMenu, StateID.mainMenu);

        // ---------- Game over flow ENDS here ---------- //
    }

    private void AddTransition(GameStateEvent stateEvent, StateID stateID)
    {
        transitionList.Add(new StateTransition2(stateEvent, stateID));
    }

    private void StartListeningToEvents()
    {
        foreach (State2 stateHandler in gameStateList)
        {
            stateHandler.StateFinishedEvent += OnStateFinished;
        }
    }

    private void StopListeningToEvents()
    {
        foreach (State2 stateHandler in gameStateList)
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
        foreach (StateTransition2 stateFlow in transitionList)
        {
            if (gameEventEnum == stateFlow.GameStateEvent)
            {
                currentGameState = stateFlow.StateID;
            }
        }
    }

    private void TransitionToNextState()
    {
        State2 gameState = GetGameStateByEnum(currentGameState);

        gameState.GameStateStarted();
    }

    private State2 GetGameStateByEnum(StateID stateID)
    {
        State2 gameState = null;

        foreach (State2 gs in gameStateList)
        {
            if (stateID == gs.StateID)
            {
                if (gameState != null)
                {
                    Debug.LogError("Multiple GameStates present of type: " + gs.StateID);
                }

                gameState = gs;
            }
        }

        if (gameState == null)
        {
            Debug.LogError("No GameState present with type: " + stateID.ToString());
        }

        return gameState;
    }

    public T GetGameState<T>() where T : GameState2
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
