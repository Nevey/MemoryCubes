using UnityEngine;
using System;

public class TargetController : MonoBehaviour
{
    [SerializeField] private ColorConfig colorConfig;

    [SerializeField] private TargetView[] targetViews;

    private bool isFirstTarget = true;

    public Color TargetColor { get; private set; }

    public static event Action TargetUpdatedEvent;   

	// Use this for pre-initialization
	private void OnEnable()
    {
        SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;

        SelectColorTargetState.SelectColorTargetStateStartedEvent += OnSelectColorTargetStateStarted;
	}

    private void OnDisable()
    {
        SetupGameState.SetupGameStateStartedEvent -= OnSetupGameStateStarted;

        SelectColorTargetState.SelectColorTargetStateStartedEvent -= OnSelectColorTargetStateStarted;
    }

    private void OnSetupGameStateStarted()
    {
        // isFirstTarget = true;
    }

    private void OnSelectColorTargetStateStarted()
    {
        SetNextTarget();
    }
    
    private void SetNextTarget()
    {
        TargetColor = colorConfig.GetRandomColor();

        for (int i = 0; i < targetViews.Length; i++)
        {
            targetViews[i].ResetTargetBar();
        }

        if (TargetUpdatedEvent != null)
        {
            TargetUpdatedEvent();
        }
    }
}
