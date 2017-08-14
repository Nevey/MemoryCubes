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

    [SerializeField] private TargetSelector targetSelector;

    [SerializeField] private TargetTime targetTime;

    private enum Direction
    {
        Left = -1,
        Right = 1
    }

    private GameObject[,] targetBarSprites;
    
    private Vector2 targetBarArraySize = Vector2.zero;

    public static event Action TargetColorUpdatedEvent;

    private void OnEnable()
    {
        targetBarArraySize.x = targetBarHeight;
        targetBarArraySize.y = targetBarWidth;
        
        targetBarSprites = new GameObject[(int)targetBarArraySize.x, (int)targetBarArraySize.y];

        targetBarPlaceholder.SetActive(false);

        targetSelector.NextTargetEvent += OnNextTargetEvent;
    }

    private void OnDisable()
    {
        DestroyTargetBar();

        targetSelector.NextTargetEvent -= OnNextTargetEvent;
    }
	
	// Update is called once per frame
	private void Update()
    {
        UpdateVisibility();
	}
    
    private void OnNextTargetEvent(object sender, NextTargetEventArgs e)
    {
        if (e.isFirstTarget)
        {
            CreateTargetBar();
        }

        UpdateTargetBar(e);
    }

    private void CreateTargetBar()
    {
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

        Destroy(targetBarPlaceholder);
    }
    
    private void UpdateTargetBar(NextTargetEventArgs e)
    {
        // Set correct color based on target (image swap)
        // TODO: Add a timer and based on time, set sprites visible/invisible
        foreach (GameObject targetBarSprite in targetBarSprites)
        {
            Image image = targetBarSprite.GetComponent<Image>();

            image.color = e.targetColor;
        }

        // Based on this event the state machine knows target color selecting is done
        // Keep it in this class in case we want to delay firing of this
        // event due to visual effects
        if (TargetColorUpdatedEvent != null)
        {
            TargetColorUpdatedEvent();
        }
    }

    private void UpdateVisibility()
    {
        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                float spritePercent = (100f / (targetBarArraySize.x + targetBarArraySize.y)) * (x + y);

                bool isActive = spritePercent < targetTime.GetTimeLeftPercent();

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
}
