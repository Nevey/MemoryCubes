using System;

public class StateFinishedArgs : EventArgs
{
    public GameStateEvent gameStateEventEnum { get; set; }
}