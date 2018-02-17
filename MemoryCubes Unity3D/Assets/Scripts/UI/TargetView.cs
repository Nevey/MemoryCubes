using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetView : MonoBehaviour, IOnUIViewInitialize
{
    [SerializeField] private GameObject targetSpritePrefab;

    [SerializeField] private GameObject targetBarPlaceholder;

    [SerializeField] private int targetBarHeight = 4;

    [SerializeField] private int targetBarWidth = 25;

    [SerializeField] private Direction direction;

    [SerializeField] private Color noTargetSelectedColor = Color.black;

    private enum Direction
    {
        Left = -1,
        Right = 1
    }

    private GameObject[,] targetBarSprites;
    
    private Vector2 targetBarArraySize = Vector2.zero;

    private bool wasVisible;

    private bool isActive;

    private Color targetColor;
	
	// Update is called once per frame
	private void Update()
    {
        if (!isActive)
        {
            return;
        }

        UpdateVisibility();
	}

    private void OnStartGameStateStarted()
    {
        targetColor = noTargetSelectedColor;

        SetupTargetView();

        ToggleTargetBarVisibility(true);

        UpdateTargetBarColor();
    }

    private void OnLevelWonStateStarted()
    {
        DisableTargetView();
    }

    private void OnGameOverStateStarted()
    {
        DisableTargetView();
    }

    private void SetupTargetView()
    {
        // Disable the placeholder (if it's not already)
        targetBarPlaceholder.SetActive(false);        

        // Create the target bar based on array size
        CreateTargetBar();

        // Toggle target bar visibilty
        ToggleTargetBarVisibility(false);

        // We're active!
        isActive = true;
    }

    private void DisableTargetView()
    {
        DestroyTargetBar();

        isActive = false;
    }

    private void CreateTargetBar()
    {
        // Create array
        targetBarArraySize.x = targetBarWidth;
        targetBarArraySize.y = targetBarHeight;
        
        targetBarSprites = new GameObject[(int)targetBarArraySize.x, (int)targetBarArraySize.y];

        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            List<int> shuffledYList = new List<int>();

            for (int i = 0; i < targetBarArraySize.y; i++)
            {
                shuffledYList.Add(i);
            }

            shuffledYList.Shuffle();

            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                GameObject targetBarSprite = Instantiate(targetSpritePrefab);

                targetBarSprite.transform.SetParent(this.transform, false);

                Image image = targetBarSprite.GetComponent<Image>();

                Vector2 scaledSizeDelta = new Vector2(
                    image.rectTransform.sizeDelta.x * image.canvas.scaleFactor,
                    image.rectTransform.sizeDelta.y * image.canvas.scaleFactor
                );

                int randomYIndex = shuffledYList[y];

                // Horizontal position is based on y and vertical position is based on placeholder position
                Vector2 position = new Vector2(
                    targetBarPlaceholder.transform.position.x + (scaledSizeDelta.x * x) * (float)direction,
                    targetBarPlaceholder.transform.position.y - (scaledSizeDelta.y * randomYIndex)
                );

                targetBarSprite.transform.position = position;

                targetBarSprites[x, y] = targetBarSprite;
            }
        }
    }

    private void ToggleTargetBarVisibility(bool isVisible)
    {
        // Prevent from looping through array if nothing will change
        if (wasVisible == isVisible)
        {
            return;
        }

        foreach (GameObject targetBarSprite in targetBarSprites)
        {
            targetBarSprite.GetComponent<Image>().enabled = isVisible;
        }

        wasVisible = isVisible;
    }
    
    private void UpdateTargetBarColor()
    {
        // Set correct color based on target (image swap)
        foreach (GameObject targetBarSprite in targetBarSprites)
        {
            Image image = targetBarSprite.GetComponent<Image>();

            image.color = targetColor;

            // TODO: see if we can do gpu-instancing here too
        }
    }

    private void UpdateVisibility()
    {
        int totalSprites = (int)targetBarArraySize.x * (int)targetBarArraySize.y;

        int index = 1;

        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                float spritePercent = ((float)index / (float)totalSprites) * 100f;

                bool isActive = spritePercent < TimeController.Instance.TimeLeftPercent;

                targetBarSprites[x, y].SetActive(isActive);

                index++;
            }
        }
    }

    private void DestroyTargetBar()
    {
        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                Destroy(targetBarSprites[x, y]);

                targetBarSprites[x, y] = null;
            }
        }
    }

    public void OnUIViewInitialize()
    {
        GameStateMachine.Instance.GetState<StartGameState>().StartEvent += OnStartGameStateStarted;

        GameStateMachine.Instance.GetState<LevelWonState>().StartEvent += OnLevelWonStateStarted;

        GameStateMachine.Instance.GetState<GameOverState>().StartEvent += OnGameOverStateStarted;
    }

    public void SetNewTargetBar(Color targetColor)
    {
        this.targetColor = targetColor;

        UpdateTargetBarColor();
    }
}
