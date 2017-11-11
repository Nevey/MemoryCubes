using System;

public class StateStartedArgs : EventArgs
{
    public GameStateEvent gameStateEventEnum { get; set; }
}