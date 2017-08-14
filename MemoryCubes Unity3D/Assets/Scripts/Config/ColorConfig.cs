using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConfig : MonoBehaviour
{
	[SerializeField] private Color[] colors;

	public Color[] Colors { get { return colors; } }

	public Color GetRandomColor()
	{
		int randomIndex = Random.Range(0, colors.Length);

		return colors[randomIndex];
	}
}
