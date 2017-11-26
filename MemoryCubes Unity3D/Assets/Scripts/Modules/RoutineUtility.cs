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
    
    public void StartWaitTimeRoutine(float time, Action callback)
    {
        StartCoroutine(WaitTimeRoutine(time, callback));
    }

    public void StartWaitOneFrameRoutine(Action callback)
    {
        StartCoroutine(WaitOneFrameRoutine(callback));
    }
}