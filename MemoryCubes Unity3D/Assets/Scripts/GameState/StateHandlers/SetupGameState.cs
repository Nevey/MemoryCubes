﻿using UnityEngine;
using System;

public class SetupGameState : GameStateHandler
{
    private Builder builder;

    public static event Action SetupGameStateStartedEvent;

    public SetupGameState(GameStateEnum gameStateEnum) : base(gameStateEnum)
    {
        
    }

    public override void GameStateStarted()
    {
        base.GameStateStarted();

        Debug.Log("SetupGameState:GameStateStarted");

        SetupGameStateStartedEvent();

        GameStateFinished();
    }

    private void GameStateFinished()
    {
        Debug.Log("SetupGameState:GameStateFinished");

        GameStateFinished(GameStateEventEnum.setupGameStateFinished);
    }
}