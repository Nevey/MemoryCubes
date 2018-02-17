using System;
using System.Collections;
using UnityEngine;

public class RoutineUtility : MonoBehaviour
{
    private IEnumerator WaitTimeRoutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);

        callback();
    }

    private IEnumerator WaitOneFrameRoutine(Action callback)
    {
        yield return new WaitForEndOfFrame();

        callback();
    }
    
    public Coroutine StartWaitTimeRoutine(float time, Action callback)
    {
        return StartCoroutine(WaitTimeRoutine(time, callback));
    }

    public Coroutine StartWaitOneFrameRoutine(Action callback)
    {
        return StartCoroutine(WaitOneFrameRoutine(callback));
    }
}