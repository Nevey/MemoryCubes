using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetView : MonoBehaviour
{
    [SerializeField] private GameObject targetSpritePrefab;

    [SerializeField] private GameObject targetBarPlaceholder;

    [SerializeField] private int targetBarHeight = 4;

    [SerializeField] private int targetBarWidth = 25;

    [SerializeField] private Direction direction;

    [SerializeField] private TargetController targetController;

    [SerializeField] private TimeController targetTime;

    private enum Direction
    {
        Left = -1,
        Right = 1
    }

    private GameObject[,] targetBarSprites;
    
    private Vector2 targetBarArraySize = Vector2.zero;

    private bool wasVisible;

    private bool isActive;

    private void OnEnable()
    {
        SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;

        GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;
    }

    private void OnDisable()
    {
        SetupGameState.SetupGameStateStartedEvent -= OnSetupGameStateStarted;

        GameOverState.GameOverStateStartedEvent -= OnGameOverStateStarted;
    }
	
	// Update is called once per frame
	private void Update()
    {
        if (!isActive)
        {
            return;
        }

        UpdateVisibility();
	}

    private void OnSetupGameStateStarted()
    {
        wasVisible = true;

        SetupTargetView();
    }

    private void OnGameOverStateStarted()
    {
        isActive = false;

        DestroyTargetBar();
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

    private void CreateTargetBar()
    {
        // Create array
        targetBarArraySize.x = targetBarHeight;
        targetBarArraySize.y = targetBarWidth;
        
        targetBarSprites = new GameObject[(int)targetBarArraySize.x, (int)targetBarArraySize.y];

        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                GameObject targetBarSprite = Instantiate(targetSpritePrefab);

                targetBarSprite.transform.SetParent(this.transform, false);

                Image image = targetBarSprite.GetComponent<Image>();

                // Horizontal position is based on y and vertical position is based on placeholder position
                Vector2 position = new Vector2(
                    targetBarPlaceholder.transform.position.x + (image.rectTransform.sizeDelta.x * y) * (float)direction,
                    targetBarPlaceholder.transform.position.y - (image.rectTransform.sizeDelta.y * x)
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
    
    private void UpdateTargetBar()
    {
        // Set correct color based on target (image swap)
        foreach (GameObject targetBarSprite in targetBarSprites)
        {
            Image image = targetBarSprite.GetComponent<Image>();

            image.color = targetController.TargetColor;

            // TODO: see if we can do gpu-instancing here too
        }
    }

    private void UpdateVisibility()
    {
        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                float spritePercent = (100f / (targetBarArraySize.x + targetBarArraySize.y)) * (x + y);

                bool isActive = spritePercent < targetTime.TimeLeftPercent;

                targetBarSprites[x, y].SetActive(isActive);
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

    public void ResetTargetBar()
    {
        ToggleTargetBarVisibility(true);

        UpdateTargetBar();
    }
}
