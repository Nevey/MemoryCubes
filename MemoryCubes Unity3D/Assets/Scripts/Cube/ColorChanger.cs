using UnityEngine;

public class ColorChanger : MonoBehaviour 
{
	private int colorCount;
	
	private float colorFadeVelocity;
	
	private Deadzone deadzone = new Deadzone();
	
	private bool isChangingColor = false;
	
	public float colorFadeSmoothTime = 1f;
	
	public float maxColorFadeSpeed = 1f;
	
	public Material[] materials;
	
	// Use this for initialization
	void Start() 
	{
		SetColorCount();
		
		ApplyRandomMaterial();
		
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
		colorCount = materials.Length;
	}
	
	private void ApplyRandomMaterial()
	{
		int randomIndex = Random.Range(0, colorCount);
		
		GetComponent<Renderer>().materials[1].color = materials[randomIndex].color;
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
			color.a = Mathf.SmoothDamp(color.a, targetAlpha, ref colorFadeVelocity, colorFadeSmoothTime, maxColorFadeSpeed);
		
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
