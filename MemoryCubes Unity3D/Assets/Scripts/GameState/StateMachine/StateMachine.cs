using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    private State currentState = null;

    private Dictionary<StateTransition, State> transitionDict = new Dictionary<StateTransition, State>();

    public static StateMachine Instance;

    public StateMachine()
    {
        Instance = this;
    }

    private KeyValuePair<StateTransition, State> GetTransitionPairByKey<T>() where T : StateTransition
    {
        for (int i = 0; i < transitionDict.Keys.Count; i++)
        {
            StateTransition stateTransition = transitionDict.Keys.ElementAt(i);

            Type type = stateTransition.GetType();

            if (type == typeof(T))
            {
                return transitionDict.ElementAt(i);
            }
        }

        Debug.LogError("Unable to find StateTransition of type: " + typeof(T).Name);

        return new KeyValuePair<StateTransition, State>();
    }

    private KeyValuePair<StateTransition, State> GetTransitionPairByValue<T>() where T : State
    {
        for (int i = 0; i < transitionDict.Keys.Count; i++)
        {
            State stateTransition = transitionDict.Values.ElementAt(i);

            Type type = stateTransition.GetType();

            if (type == typeof(T))
            {
                return transitionDict.ElementAt(i);
            }
        }

        Debug.LogError("Unable to find StateTransition of type: " + typeof(T).Name);

        return new KeyValuePair<StateTransition, State>();
    }

    protected void AddTransition<TStateTransition, TState>()
        where TStateTransition : StateTransition, new()
        where TState : State, new()
    {
        transitionDict.Add(new TStateTransition(), new TState());
    }

    public void DoTransition<T>() where T : StateTransition
    {
        KeyValuePair<StateTransition, State> transitionPair = GetTransitionPairByKey<T>();

        transitionPair.Value.Start();

        currentState = transitionPair.Value;
    }

    public T GetTransition<T>() where T : StateTransition
    {
        KeyValuePair<StateTransition, State> transitionPair = GetTransitionPairByKey<T>();

        return transitionPair.Key as T;
    }

    public T GetState<T>() where T : State
    {
        KeyValuePair<StateTransition, State> transitionPair = GetTransitionPairByValue<T>();

        return transitionPair.Value as T;
    }
}