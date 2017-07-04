using UnityEngine;
using System;

public class ColorChangeReadyEventArgs : EventArgs
{
    public GameObject cube { get; set; }
}

public class ColorChanger : MonoBehaviour 
{
	private int materialCount;
	
	private float colorFadeVelocity;
	
	private Deadzone deadzone = new Deadzone();
	
	private bool isChangingColor = false;
    
    private Material selectedMaterial;
    
    private Material[] storedMaterials;
	
	public float colorFadeSmoothTime = 1f;
	
	public float maxColorFadeSpeed = 1f;
	
	public Material[] materials;
    
    public event EventHandler<ColorChangeReadyEventArgs> ColorChangeReadyEvent;
	
	// Use this for initialization
	void Start() 
	{
        StoreMaterials();
        
		SetColorCount();
		
		ApplyRandomMaterial();
		
		StartColorChange();
	}
	
	// Update is called once per frame
	void Update() 
	{
		UpdateColor();
	}
    
    private void StoreMaterials()
    {
        // Store the materials array before doing anything
        storedMaterials = GetComponent<Renderer>().materials;
    }
	
	private void SetColorCount()
	{
		// TODO: based on level editor/difficulty
		materialCount = materials.Length;
	}
	
	private void ApplyRandomMaterial()
	{
		int randomIndex = UnityEngine.Random.Range(0, materialCount);
        
        selectedMaterial = materials[randomIndex];
        
        GetComponent<Renderer>().materials = new Material[] { storedMaterials[0], selectedMaterial };
	}
    
    private void StartColorChange()
	{
		isChangingColor = true;
	}
	
	private void UpdateColor()
	{
		if (!isChangingColor)
		{
			return;
		}
		
        // We're not really changing color, but just fading out the white material on top...
        
		Color color = GetComponent<Renderer>().materials[0].color;
		
		float targetAlpha = 0f;

        color.a = Mathf.SmoothDamp(
                color.a,
                targetAlpha,
                ref colorFadeVelocity,
                colorFadeSmoothTime,
                maxColorFadeSpeed);

        GetComponent<Renderer>().materials[0].color = color;

        // Use a bigger deadzone to send color change ready event
        if (!deadzone.OutOfReach(color.a, targetAlpha, 0.2f))
		{
            ColorChangeReady();
        }

        // Use a smaller deadzone so the color change keeps going after the event was sent
        // Otherwise the difference between current and target color may be too big
        if (!deadzone.OutOfReach(color.a, targetAlpha))
        {
            isChangingColor = false;
        }
	}
	
	private void ColorChangeReady()
	{
        // Replace materials array with a new array, containing only the dynamic material
 		GetComponent<Renderer>().materials = new Material[] { selectedMaterial };
        
        ColorChangeReadyEventArgs args = new ColorChangeReadyEventArgs();
        
        args.cube = this.gameObject;
        
        if (ColorChangeReadyEvent != null)
        {
            ColorChangeReadyEvent(this, args);
        }
	}
}
