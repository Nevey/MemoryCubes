using System;
using UnityEngine;


public class State
{
    public event Action StartEvent;

    public event Action FinishedEvent;

    public State()
    {
        
    }

    /// <summary>
    /// Called by the state machine when transitioning in to this state
    /// </summary>
    public virtual void Start()
    {
        Debug.LogFormat("{0}:Start", this.GetType().Name);

        if (StartEvent != null)
        {
            StartEvent();
        }
    }

    /// <summary>
    /// Called by the state machine when transitioning away from this state
    /// </summary>
    public virtual void Finish()
    {
        Debug.LogFormat("{0}:Finish", this.GetType().Name);

        if (FinishedEvent != null)
        {
            FinishedEvent();
        }
    }
}