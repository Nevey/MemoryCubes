using UnityEngine;
using System.Collections;
using System;
using UnityTools.Base;

public class TimeController : MonoBehaviourSingleton<TimeController>
{
    [SerializeField] private TimeConfig timeConfig;

    private float currentTime = 0f;
    
    private bool isActive = false;

    public float TimeLeftPercent
    {
        get
        {
            return (100f / GetMaxTimeForCurrentLevel()) * currentTime;
        }
    }

    public event Action OutOfTimeEvent;

    private void OnEnable()
    {
        GameStateMachine.Instance.GetState<SetupGameState>().StartEvent += OnSetupGameStateStarted;

        GameStateMachine.Instance.GetState<DestroyRemainingCubesState>().StartEvent += OnDestroyRemainingCubesStateStarted;
    }

    private void OnDisable()
    {
        GameStateMachine.Instance.GetState<SetupGameState>().StartEvent -= OnSetupGameStateStarted;

        GameStateMachine.Instance.GetState<DestroyRemainingCubesState>().StartEvent -= OnDestroyRemainingCubesStateStarted;
    }

    private void FixedUpdate()
    {
        if (!isActive)
        {
            return;
        }

        UpdateCurrentTime();
	}

    private void OnSetupGameStateStarted()
    {
        ResetTimer();

        StartTimer();
    }

    private void OnDestroyRemainingCubesStateStarted()
    {
        StopTimer();
    }

    private void ResetTimer()
    {
        currentTime = GetMaxTimeForCurrentLevel();
    }

    private void StartTimer()
    {
        isActive = true;
    }

    private void StopTimer()
    {
        isActive = false;
    }

    private void UpdateCurrentTime()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0f)
        {
            currentTime = 0f;

            if (OutOfTimeEvent != null)
            {
                OutOfTimeEvent();
            }
        }
    }

    private float GetMaxTimeForCurrentLevel()
    {
        int index = LevelController.Instance.CurrentLevel + 1;

        // float maxTime = timeConfig.LevelTimeCurve.keys[index].value;
        float maxTime = timeConfig.LevelTimeCurve.Evaluate(index);

        return maxTime;
    }

    public void ApplyTileBonus()
    {
        currentTime += timeConfig.BonusTimePerTile;

        float maxTime = GetMaxTimeForCurrentLevel();

        if (currentTime > maxTime)
        {
            currentTime = maxTime;
        }
    }

    public void ApplyTilePenalty()
    {
        currentTime -= timeConfig.PenaltyPerTile;
    }

    public void ApplyPenaltyNoSelectedTiles()
    {
        currentTime -= timeConfig.PenaltyNoSelectedTiles;
    }
}
