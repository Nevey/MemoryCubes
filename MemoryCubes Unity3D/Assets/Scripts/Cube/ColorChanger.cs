using UnityEngine;

public class ColorChanger : MonoBehaviour 
{
	private int materialCount;
	
	private float colorFadeVelocity;
	
	private Deadzone deadzone = new Deadzone();
	
	private bool isChangingColor = false;
    
    private Material selectedMaterial;
	
	public float colorFadeSmoothTime = 1f;
	
	public float maxColorFadeSpeed = 1f;
	
	public Material[] materials;
    
    public Material SelectedMaterial
    {
        get { return selectedMaterial; }
    }
	
	// Use this for initialization
	void Awake() 
	{
		SetColorCount();
		
		ApplyRandomColorToMaterial();
		
		OnStartColorChange();
	}
	
	// Update is called once per frame
	void Update() 
	{
		FadeWhiteMaterial();
	}
	
	private void SetColorCount()
	{
		// TODO: based on level editor/difficulty
		materialCount = materials.Length;
	}
	
	private void ApplyRandomColorToMaterial()
	{
		int randomIndex = Random.Range(0, materialCount);
        
        selectedMaterial = materials[randomIndex];
        
		GetComponent<Renderer>().materials[1].color = selectedMaterial.color;
	}
	
	private void FadeWhiteMaterial()
	{
		if (!isChangingColor)
		{
			return;
		}
		
		Color color = GetComponent<Renderer>().materials[0].color;
		
		float targetAlpha = 0f;
		
		if (deadzone.OutOfReach(color.a, targetAlpha))
		{
			color.a = Mathf.SmoothDamp(
                color.a, 
                targetAlpha, 
                ref colorFadeVelocity, 
                colorFadeSmoothTime, 
                maxColorFadeSpeed);
		
			GetComponent<Renderer>().materials[0].color = color;
		}
		else
		{
			OnColorChangeReady();
		}
	}
	
	private void OnStartColorChange()
	{
		isChangingColor = true;
	}
	
	private void OnColorChangeReady()
	{
		isChangingColor = false;
		
		// Get materials array
		Material[] materials = GetComponent<Renderer>().materials;
		
		// Replace materials array with a new array, containing only the dynamic material
 		GetComponent<Renderer>().materials = new Material[] { materials[1] };
	}
}
