using System;
using UnityEngine;

public class GridAnimator : MonoBehaviour
{
    [SerializeField] private RoutineUtility routineUtility;

    [SerializeField] private float minWaitTime = 5f;

    [SerializeField] private float maxWaitTime = 5.5f;

    [SerializeField] private float delayPerTile = 0.1f;

    private Coroutine timerRoutine;

    private void Awake()
    {
        GameStateMachine.Instance.GetState<PlayerInputState>().StartEvent += OnPlayerInputStateStarted;

        GameStateMachine.Instance.GetState<DestroyRemainingCubesState>().StartEvent += OnDestroyRemainingCubesStateStarted;
    }

    private void OnPlayerInputStateStarted()
    {
        // StartTimer();
    }

    private void OnDestroyRemainingCubesStateStarted()
    {
        if (timerRoutine == null)
        {
            return;
        }

        routineUtility.StopCoroutine(timerRoutine);

        timerRoutine = null;
    }

    private void StartTimer()
    {
        float waitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);

        timerRoutine = routineUtility.StartWaitTimeRoutine(waitTime, DoGridScaleAnimation);
    }

    private void DoGridScaleAnimation()
    {
        GameObject[,,] grid = GridBuilder.Instance.Grid;

        for (int x = 0; x < GridBuilder.Instance.GridSize; x++)
        {
            for (int y = 0; y < GridBuilder.Instance.GridSize; y++)
            {
                for (int z = 0; z < GridBuilder.Instance.GridSize; z++)
                {
                    int delayIndex = x + y + z;

                    float delay = delayPerTile * delayIndex;

                    GameObject tile = GridBuilder.Instance.Grid[x, y, z];

                    if (tile == null)
                    {
                        continue;
                    }

                    tile.GetComponent<Resizer>().DoGridScaleUpTween(delay);
                }
            }
        }

        StartTimer();
    }
}