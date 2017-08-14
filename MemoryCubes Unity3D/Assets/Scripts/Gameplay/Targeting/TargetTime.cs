using UnityEngine;
using System.Collections;
using System;

public class TargetTime : MonoBehaviour
{
    [SerializeField] private float maxTime = 5f;

    [SerializeField] private float scalePerLevel = 1f;

    private float currentTime = 0f;
    
    private bool isActive = false;

    public event Action OutOfTimeEvent;

    private void OnEnable()
    {
        PlayerInputState.PlayerInputStateStartedEvent += OnPlayerInputStateStarted;
    }

    private void OnDisable()
    {
        PlayerInputState.PlayerInputStateStartedEvent -= OnPlayerInputStateStarted;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (!isActive)
        {
            return;
        }

        UpdateCurrentTime();
	}

    private void OnPlayerInputStateStarted()
    {
        ResetTimer();
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

    private void ResetTimer()
    {
        currentTime = maxTime;

        isActive = true;
    }

    public float GetTimeLeftPercent()
    {
        float timeLeftPercent = (100f / maxTime) * currentTime;

        return timeLeftPercent;
    }
}
