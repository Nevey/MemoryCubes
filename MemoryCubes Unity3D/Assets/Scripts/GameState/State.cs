using System;
using UnityEngine;

public abstract class State2
{
    public StateID StateID { get; private set; }

    public event EventHandler<StateStartedArgs> StateStartedEvent;

    public event EventHandler<StateFinishedArgs> StateFinishedEvent;

    public State2(StateID stateID)
    {
        StateID = stateID;
    }

    /// <summary>
    /// Will be called whenever the state handler is started
    /// </summary>
    public virtual void GameStateStarted()
    {
        Debug.LogFormat("{0}:GameStateStarted", this.GetType().Name);

        StateStartedArgs args = new StateStartedArgs();

        if (StateStartedEvent != null)
        {
            StateStartedEvent(this, args);
        }
    }

    /// <summary>
    /// Use to end state and send an event
    /// </summary>
    /// <param name="gameStateEventEnum"></param>
    protected void GameStateFinished(GameStateEvent gameStateEventEnum)
    {
        Debug.LogFormat("{0}:GameStateFinished", this.GetType().Name);

        StateFinishedArgs args = new StateFinishedArgs();

        args.gameStateEventEnum = gameStateEventEnum;

        if (StateFinishedEvent != null)
        {
            StateFinishedEvent(this, args);
        }
    }
}