using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridConfig))]
public class ColorConfig : MonoBehaviour
{
	[SerializeField] private Color[] colors;

	public Color[] Colors { get { return colors; } }

	public Color GetRandomColor()
	{
		GridConfig gridConfig = GetComponent<GridConfig>();

		int gridColorCount = (int)gridConfig.GridColorCount.Evaluate(LevelController.Instance.CurrentLevel);

		int maxColorCount = gridColorCount < colors.Length ? gridColorCount : colors.Length;
		
		int randomIndex = Random.Range(0, maxColorCount);

		return colors[randomIndex];
	}
}
