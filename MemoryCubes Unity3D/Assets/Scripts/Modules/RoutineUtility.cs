using System;
using System.Collections;
using UnityEngine;

public class RoutineUtility : MonoBehaviour
{
    private IEnumerator WaitRoutine(float time, Action callback)
    {
        yield return new WaitForSeconds(time);

        callback();
    }
    
    public void StartWaitRoutine(float time, Action callback)
    {
        StartCoroutine(WaitRoutine(time, callback));
    }
}