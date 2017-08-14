using UnityEngine;
using System;

public class PlayerInputState : GameStateHandler
{
    public static event Action PlayerInputStateStartedEvent;

    public PlayerInputState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SelectingCubesState:GameStateStarted");

        DestroyController.DestroyFinishedEvent += OnDestroyFinished;

        PlayerInputStateStartedEvent();
    }

    private void OnDestroyFinished()
    {
        Debug.Log("SelectingCubesState:OnCollect");

        DestroyController.DestroyFinishedEvent -= OnDestroyFinished;

        GameStateFinished(GameStateEventEnum.playerInputStateFinished);
    }
}