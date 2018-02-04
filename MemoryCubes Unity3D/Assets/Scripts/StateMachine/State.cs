using System;
using UnityEngine;


public abstract class State
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

        PreStart();

        if (StartEvent != null)
        {
            StartEvent();
        }

        PostStart();
    }

    /// <summary>
    /// Called by the state machine when transitioning away from this state
    /// </summary>
    public virtual void Finish()
    {
        Debug.LogFormat("{0}:Finish", this.GetType().Name);

        PreFinish();

        if (FinishedEvent != null)
        {
            FinishedEvent();
        }

        PostFinish();
    }

    protected abstract void PreStart();

    protected abstract void PostStart();

    protected abstract void PreFinish();

    protected abstract void PostFinish();
}