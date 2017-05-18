using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFinder
{
    public SpriteFinder()
    {

    }

    public Sprite FindSprite(TargetColors color)
    {
        string name = color.ToString();

        return FindSprite(name);
    }

    public Sprite FindSprite(string name)
    {
        return Resources.Load<Sprite>("Images/TargetBar/" + name);
    }
}

public class TargetPainter : MonoBehaviour
{
    [SerializeField] private GameObject targetSpritePrefab;

    [SerializeField] private GameObject targetBarPlaceholder;

    [SerializeField] private int targetBarHeight = 4;

    [SerializeField] private int targetBarWidth = 25;

    [SerializeField] private int direction;

    [SerializeField] private TargetSelector targetSelector;

    private GameObject[,] targetBarSprites;
    
    private Vector2 targetBarArraySize = Vector2.zero;

    private SpriteFinder spriteFinder = new SpriteFinder();

	// Use this for initialization
	void Start() 
    {
        targetBarArraySize.x = targetBarHeight;
        targetBarArraySize.y = targetBarWidth;
        
        targetBarSprites = new GameObject[(int)targetBarArraySize.x, (int)targetBarArraySize.y];

        targetBarPlaceholder.SetActive(false);

        targetSelector.NextTargetEvent += OnNextTargetEvent;
	}
	
	// Update is called once per frame
	void Update() 
    {
	
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
                    targetBarPlaceholder.transform.position.x + (image.rectTransform.sizeDelta.x * y) * direction,
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
        // Add a timer and based on time, set sprites visible/invisible

        foreach(GameObject targetBarSprite in targetBarSprites)
        {
            targetBarSprite.GetComponent<Image>().sprite = spriteFinder.FindSprite(e.targetColor);
        }
    }
}
