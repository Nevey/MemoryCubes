using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridColoring : MonoBehaviour
{
	[SerializeField] private Builder builder;

	[SerializeField] private ColorConfig colorConfig;

	private MaterialPropertyBlock propertyBlock;

	// Use this for initialization
	private void Start()
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
			propertyBlock.SetColor("_Color", colorConfig.GetRandomColor());

			Renderer tileRenderer = builder.FlattenedGridList[i].GetComponent<Renderer>();

			tileRenderer.SetPropertyBlock(propertyBlock);
		}
	}
}
