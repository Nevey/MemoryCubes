using UnityEngine;
using System;
using System.Collections.Generic;

public class TargetController : MonoBehaviour
{
    [SerializeField] private ColorConfig colorConfig;

    [SerializeField] private Builder builder;

    [SerializeField] private TargetView[] targetViews;

    private bool isFirstTarget = true;

    private Color targetColor;

    private Color previousTargetColor;

    public Color TargetColor { get { return targetColor; } }

    public static event Action TargetUpdatedEvent;   

	// Use this for pre-initialization
	private void OnEnable()
    {
        SelectColorTargetState.SelectColorTargetStateStartedEvent += OnSelectColorTargetStateStarted;
	}

    private void OnDisable()
    {
        SelectColorTargetState.SelectColorTargetStateStartedEvent -= OnSelectColorTargetStateStarted;
    }

    private void OnSelectColorTargetStateStarted()
    {
        SetNextTarget();
    }
    
    private void SetNextTarget()
    {
        // Get a random target color
        targetColor = GetRandomColor();

        // Set the previous target color
        previousTargetColor = targetColor;

        // Reset the target bars
        for (int i = 0; i < targetViews.Length; i++)
        {
            targetViews[i].ResetTargetBar();
        }        

        // We're ready, send target updated event
        if (TargetUpdatedEvent != null)
        {
            TargetUpdatedEvent();
        }
    }

    /// <summary>
    /// Returns a random active color, if there isn't any, returns the previous color
    /// </summary>
    /// <returns></returns>
    private Color GetRandomColor()
    {
        List<Color> activeColors = GetActiveColors();

        // If no color was found in the list, use previously used color
        if (activeColors.Count == 0)
        {
            return previousTargetColor;
        }

        int randomIndex = UnityEngine.Random.Range(0, activeColors.Count);

        return activeColors[randomIndex];
    }

    /// <summary>
    /// Filters out previous target color and colors that are not found in the grid
    /// </summary>
    /// <returns>List<Color></returns>
    private List<Color> GetActiveColors()
    {
        List<Color> activeColors = new List<Color>();

        for (int i = 0; i < builder.FlattenedGridList.Count; i++)
        {
            GameObject tile = builder.FlattenedGridList[i];

            Color tileColor = tile.GetComponent<TileColor>().MyColor;

            if (activeColors.Contains(tileColor))
            {
                continue;
            }

            activeColors.Add(tileColor);
        }

        if (activeColors.Contains(previousTargetColor))
        {
            activeColors.Remove(previousTargetColor);
        }

        return activeColors;
    }
}
