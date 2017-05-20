using UnityEngine;
using System;

public class PlayerSelectingCubesState : GameStateHandler
{
    public static event Action SelectingCubesStateStartedEvent;

    public PlayerSelectingCubesState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SelectingCubesState:GameStateStarted");

        GridCollector.CollectEvent += OnCollect;

        SelectingCubesStateStartedEvent();
    }

    private void OnCollect()
    {
        Debug.Log("SelectingCubesState:OnCollect");

        GridCollector.CollectEvent -= OnCollect;

        GameStateFinished(GameStateEventEnum.playerSelectingCubesReady);
    }
}