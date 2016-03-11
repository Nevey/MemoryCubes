using UnityEngine;
using UnityEngine.UI;

public class TargetPainter : MonoBehaviour {
    
    private GameObject[,] targetBarSprites;
    
    private Vector2 targetBarArraySize = Vector2.zero;
    
    public GameObject targetSpritePrefab;
    
    public GameObject[] targetBarPlaceholders;
    
    public int targetBarLength = 25;
    
    public int direction;

	// Use this for initialization
	void Start() 
    {
        targetBarArraySize.x = targetBarPlaceholders.Length;
        targetBarArraySize.y = targetBarLength;
        
        targetBarSprites = new GameObject[(int)targetBarArraySize.x, (int)targetBarArraySize.y];
        
        for (int x = 0; x < targetBarArraySize.x; x++)
        {
            for (int y = 0; y < targetBarArraySize.y; y++)
            {
                GameObject targetBarSprite = GameObject.Instantiate(targetSpritePrefab);
                
                targetBarSprite.transform.SetParent(this.transform, false);
                
                Image image = targetBarSprite.GetComponent<Image>();
                
                // Horizontal position is based on y and vertical position is based on placeholder position
                Vector2 position = new Vector2(
                    targetBarPlaceholders[x].transform.position.x + (image.rectTransform.sizeDelta.x * y) * direction,
                    targetBarPlaceholders[x].transform.position.y
                );
                
                targetBarSprite.transform.position = position;
                
                targetBarSprites[x, y] = targetBarSprite;
            }
        }
        
        for (int i = 0; i < targetBarPlaceholders.Length; i++)
        {
            Destroy(targetBarPlaceholders[i]);
        }
        
	    GameObject.Find("Game").GetComponent<TargetSelector>().NextTargetEvent += OnNextTargetEvent;
	}
	
	// Update is called once per frame
	void Update() 
    {
	
	}
    
    private void OnNextTargetEvent(object sender, NextTargetEventArgs e)
    {
        CreateTargetBar(e);
    }
    
    private void CreateTargetBar(NextTargetEventArgs e)
    {
        // Set correct color based on target (image swap)
        // Add a timer and based on time, set sprites visible/invisible
    }
}
