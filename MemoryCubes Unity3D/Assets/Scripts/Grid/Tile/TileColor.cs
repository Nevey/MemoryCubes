using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColor : MonoBehaviour
{
	private Color myColor;

	public Color MyColor { get { return myColor; } }

	public void SetColor(Color color)
	{
		myColor = color;
	}
}
