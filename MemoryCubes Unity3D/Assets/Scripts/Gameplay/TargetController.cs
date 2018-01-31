using UnityEngine;
using System;
using System.Collections.Generic;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameStateController gameStateController;

    [SerializeField] private GridBuilder gridBuilder;

    [SerializeField] private TargetView[] targetViews;

    [SerializeField] private FreeTileChecker freeTileChecker;

    [SerializeField] private GameModeController gameModeController;

    private Color targetColor;

    private Color previousTargetColor;

    /// <summary>
    /// Use this color to assign "no target color"
    /// </summary>
    private Color noTarget = Color.black;

    public Color TargetColor { get { return targetColor; } }

    public event Action TargetUpdatedEvent;

    public event Action NoTargetFoundEvent;

	private void OnEnable()
    {
        // SelectColorTargetState.SelectColorTargetStateStartedEvent += OnSelectColorTargetStateStarted;

        // TODO:
        gameStateController.GetGameState<SelectColorTargetState>().StateStartedEvent += OnSelectColorTargetStateStarted;
	}

    private void OnDisable()
    {
        // SelectColorTargetState.SelectColorTargetStateStartedEvent -= OnSelectColorTargetStateStarted;

        gameStateController.GetGameState<SelectColorTargetState>().StateStartedEvent -= OnSelectColorTargetStateStarted;
    }

    private void OnSelectColorTargetStateStarted(object sender, StateStartedArgs e)
    {
        // SetNextTargetRandom();
    }
    
    private void SetNextTargetRandom()
    {
        // Get a random target color
        targetColor = GetRandomColor();

        // Set the previous target color
        previousTargetColor = targetColor;

        // Reset the target bars
        for (int i = 0; i < targetViews.Length; i++)
        {
            targetViews[i].SetNewTargetBar(targetColor);
        }

        // We're ready, send target updated event
        if (TargetUpdatedEvent != null)
        {
            TargetUpdatedEvent();
        }
    }

    private void WaitForTarget()
    {
        targetColor = noTarget;
    }

    /// <summary>
    /// Returns a random active color, if there isn't any, returns the previous color
    /// </summary>
    /// <returns></returns>
    private Color GetRandomColor()
    {
        List<Color> activeColors = GetFilteredListByGameMode(GetActiveColors());

        if (activeColors.Count == 0)
        {
            switch (gameModeController.CurrentGameMode)
            {
                case GameMode.Combine:

                    // Unable to find a new target
                    if (NoTargetFoundEvent != null)
                    {
                        NoTargetFoundEvent();
                    }
                    
                    return previousTargetColor;
                
                default:
                    return previousTargetColor;
            }
        }

        int randomIndex = UnityEngine.Random.Range(0, activeColors.Count);

        return activeColors[randomIndex];
    }

    /// <summary>
    /// Filters out target colors based on a bunch of specific rules
    /// </summary>
    /// <returns>List<Color></returns>
    private List<Color> GetActiveColors()
    {
        List<Color> activeColors = new List<Color>();

        for (int i = 0; i < gridBuilder.FlattenedGridList.Count; i++)
        {
            GameObject tile = gridBuilder.FlattenedGridList[i];

            // Don't add a color that's already in the list
            Color tileColor = tile.GetComponent<TileColor>().MyColor;

            if (activeColors.Contains(tileColor))
            {
                continue;
            }

            // Don't add the tile's color if it cannot be tapped by the user
            GridCoordinates gridCoordinates = tile.GetComponent<GridCoordinates>();

            if (!freeTileChecker.CanTapTile(gridCoordinates.MyPosition))
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

    private List<Color> GetFilteredListByGameMode(List<Color> colorList)
    {
        switch (gameModeController.CurrentGameMode)
        {            
            case GameMode.Combine:
                return GetCombineGameModeColorList(colorList);
            
            default:
                return colorList;
        }
    }

    private List<Color> GetCombineGameModeColorList(List<Color> colorList)
    {
        List<Color> combineColorList = new List<Color>();

        for (int i = 0; i < colorList.Count; i++)
        {
            Color color = colorList[i];

            int colorCount = GetColorCount(color);

            if (colorCount < 2)
            {
                continue;
            }

            combineColorList.Add(color);
        }

        return combineColorList;
    }

    private int GetColorCount(Color targetColor)
    {
        int colorCount = 0;

        for (int i = 0; i < gridBuilder.FlattenedGridList.Count; i++)
        {
            TileColor tileColor = gridBuilder.FlattenedGridList[i].GetComponent<TileColor>();

            if (tileColor.MyColor == targetColor)
            {
                colorCount++;
            }
        }

        return colorCount;
    }
    
    public void SetNextTarget(Color color)
    {
        if (targetColor == color)
        {
            return;
        }

        targetColor = color;

        // Set the previous target color
        previousTargetColor = targetColor;

        // Reset the target bars
        for (int i = 0; i < targetViews.Length; i++)
        {
            targetViews[i].SetNewTargetBar(targetColor);
        }

        if (TargetUpdatedEvent != null)
        {
            TargetUpdatedEvent();
        }
    }
}
