using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridColoring : MonoBehaviour
{
	[SerializeField] private Builder builder;

	[SerializeField] private ColorConfig colorConfig;

	private MaterialPropertyBlock propertyBlock;

	// Use this for initialization
	private void Awake()
	{
		propertyBlock = new MaterialPropertyBlock();

		builder.GridBuildFinishedEvent += OnGridBuildFinished;
	}
	
	// Update is called once per frame
	private void Update()
	{
		
	}

	private void OnGridBuildFinished(object sender, GridBuildFinishedEventArgs args)
	{
		SetupColors();
	}

	/// <summary>
	/// Get random color for each tile and set color via gpu-instancing
	/// </summary>
	private void SetupColors()
	{
		for (int i = 0; i < builder.FlattenedGridList.Count; i++)
		{
			// Get the tile
			GameObject tile = builder.FlattenedGridList[i];

			// Get a random color
			Color randomColor = colorConfig.GetRandomColor();

			// Set the color for the property block
			propertyBlock.SetColor("_Color", randomColor);

			// Apply property block to tile renderer
			tile.GetComponent<Renderer>().SetPropertyBlock(propertyBlock);

			// Store the selected tile color for easy access
			tile.GetComponent<TileColor>().SetColor(randomColor);
		}
	}
}
