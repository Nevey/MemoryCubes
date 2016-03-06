using UnityEngine;

public class Viewport : MonoBehaviour 
{
	private float scale;
	
	public Vector2 designRect;

	void Awake() 
	{
		CalculateScale();
	}
	
	void Update()
	{
		// TODO: find a way to do this on screen size change only!
		// CalculateScale();
	}
	
	private void CalculateScale()
	{
		float designPixelCount = designRect.x * designRect.y;
		
		float pixelCount = Screen.width * Screen.height;
		
		scale = pixelCount / designPixelCount;
		
		Debug.Log("Viewport scale: " + scale);
	}
	
	public float Scale
	{
		get { return scale; }
	}
}
