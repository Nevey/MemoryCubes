using UnityEngine;
using System.Collections;
using System;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TimeConfig timeConfig;

    [SerializeField] private LevelController levelController;

    private float currentTime = 0f;
    
    private bool isActive = false;

    public float TimeLeftPercent
    {
        get
        {
            return (100f / GetMaxTimeForCurrentLevel()) * currentTime;
        }
    }

    public static event Action OutOfTimeEvent;

    private void OnEnable()
    {
        SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;
    }

    private void OnDisable()
    {
        SetupGameState.SetupGameStateStartedEvent -= OnSetupGameStateStarted;
    }

    // Update is called once per frame
    private void Update()
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
    }

    private void ResetTimer()
    {
        currentTime = GetMaxTimeForCurrentLevel();

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
        int index = levelController.CurrentLevel + 1;

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
