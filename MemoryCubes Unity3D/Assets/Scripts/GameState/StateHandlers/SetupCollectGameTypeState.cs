using UnityEngine;
using System;

public class SetupCollectGameModeState : GameState
{
    public SetupCollectGameModeState(GameStateType gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SetupCollectGameModeState:GameStateStarted");

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