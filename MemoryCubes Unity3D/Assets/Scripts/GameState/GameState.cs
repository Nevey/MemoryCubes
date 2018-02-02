using System;
using UnityEngine;

public abstract class GameState2 : State2
{
    protected UIController uiController;

    public GameState2(StateID stateID) : base(stateID)
    {
        uiController = MonoBehaviour.FindObjectOfType<UIController>();
    }
}