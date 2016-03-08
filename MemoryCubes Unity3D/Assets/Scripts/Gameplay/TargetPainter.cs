using UnityEngine;

public class TargetPainter : MonoBehaviour {

	// Use this for initialization
	void Start() 
    {
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
        // Create 2 dimensional array with target fracture sprites
        // Make the placeholder sprites invisible
        // Start positioning based on those placeholder sprites and move outwards on x
        
        // Add a timer and based on time, set sprites visible/invisible
        
        Sprite targetSprite = LoadSprite(e.targetColor);
    }
    
    private Sprite LoadSprite(TargetColors targetColor)
    {
        Sprite sprite = (Sprite)Resources.Load(targetColor.ToString());
        
        return sprite;
    }
}
