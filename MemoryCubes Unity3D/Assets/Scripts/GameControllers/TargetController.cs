using UnityEngine;
using System;
using System.Collections.Generic;
using UnityTools.Base;

public class TargetController : MonoBehaviourSingleton<TargetController>
{
    [SerializeField] private TargetView[] targetViews;

    [SerializeField] private FreeTileChecker freeTileChecker;

    private Color targetColor = Color.magenta;

    private Color previousTargetColor;

    public Color TargetColor { get { return targetColor; } }

    public event Action TargetUpdatedEvent;

    public event Action NoTargetFoundEvent;
    
    private void SetNextTargetRandom()
    {
        // Get a random target color
        targetColor = GetRandomColor();

        SetNextTarget(targetColor);
    }

    /// <summary>
    /// Returns a random active color, if there isn't any, returns the previous color
    /// </summary>
    /// <returns>Color</returns>
    private Color GetRandomColor()
    {
        List<Color> activeColors = GetFilteredListByGameMode(GetActiveColors());

        if (activeColors.Count == 0)
        {
            switch (GameModeController.Instance.CurrentGameMode)
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

        for (int i = 0; i < GridBuilder.Instance.FlattenedGridList.Count; i++)
        {
            GameObject tile = GridBuilder.Instance.FlattenedGridList[i];

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

        return activeColors;
    }

    private List<Color> GetFilteredListByGameMode(List<Color> colorList)
    {
        switch (GameModeController.Instance.CurrentGameMode)
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

            int colorCount = GetCubeCountWithColor(color);

            if (colorCount < 2)
            {
                continue;
            }

            combineColorList.Add(color);
        }

        return combineColorList;
    }

    private int GetCubeCountWithColor(Color targetColor)
    {
        int colorCount = 0;

        for (int i = 0; i < GridBuilder.Instance.FlattenedGridList.Count; i++)
        {
            TileColor tileColor = GridBuilder.Instance.FlattenedGridList[i].GetComponent<TileColor>();

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

    public int GetTargetColorCount()
    {
        return GetActiveColors().Count;
    }
}
