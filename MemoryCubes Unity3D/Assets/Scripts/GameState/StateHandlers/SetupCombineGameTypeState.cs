using UnityEngine;
using System;

public class SetupCombineGameModeState : GameState
{

    public SetupCombineGameModeState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        Debug.Log("SetupCombineGameModeState:GameStateStarted");

        // MainMenuView.CollectModePressedEvent += OnCollectModePressed;

        // MainMenuView.CombineModePressedEvent += OnCombineModePressed;
    }

    // private void OnCollectModePressed()
    // {
    //     Debug.Log("MainMenuState:OnCollectModePressed");

    //     MainMenuView.CollectModePressedEvent -= OnCollectModePressed;

    //     MainMenuView.CombineModePressedEvent -= OnCombineModePressed;

    //     GameStateFinished(GameStateEventEnum.startCollectGame);
    // }
}