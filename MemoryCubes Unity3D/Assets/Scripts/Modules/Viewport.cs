using UnityEngine;

public class Viewport : MonoBehaviour 
{
	[SerializeField] private Canvas canvas;

	public float Scale
	{
		get { return canvas.scaleFactor; }
	}
}
