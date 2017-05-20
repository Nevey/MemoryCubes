using System;

public class StateFinishedArgs : EventArgs
{
    public GameStateEventEnum gameStateEventEnum { get; set; }
}