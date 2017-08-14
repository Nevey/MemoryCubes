using UnityEngine;
using System.Collections;
using System;

public class TargetTime : MonoBehaviour
{
    [SerializeField] private float maxTime = 5f;

    [SerializeField] private float bonusStep = 0.1f;

    [SerializeField] private float penaltyStep = 0.3f;

    private float currentTime = 0f;
    
    private bool isActive = false;

    public float TimeLeftPercent
    {
        get
        {
            return (100f / maxTime) * currentTime;
        }
    }

    public event Action OutOfTimeEvent;

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
        UpdateCurrentTime();
	}

    private void OnSetupGameStateStarted()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        currentTime = maxTime;

        isActive = true;
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

    public void ApplyBonus()
    {
        currentTime += bonusStep;
    }

    public void ApplyPenalty()
    {
        currentTime -= penaltyStep;
    }
}
