using UnityEngine;
using System;
using System.Collections.Generic;

public class GridBuildFinishedEventArgs : EventArgs
{

}

public class BuilderReadyEventArgs : EventArgs
{
    
}

public class Builder : MonoBehaviour 
{
	[SerializeField] private int gridSize = 3;
	
	[SerializeField] private GameObject tilePrefab;
	
	[SerializeField] private float spaceBetweenTiles = 0.2f;
	
	public int GridSize { get { return gridSize; } }

	private GameObject[,,] grid;

	public GameObject[,,] Grid { get { return grid; } }

	public List<GameObject> FlattenedGridList {	get; private set; }
    
	public event EventHandler<GridBuildFinishedEventArgs> GridBuildFinishedEvent;

    public static event EventHandler<BuilderReadyEventArgs> BuilderReadyEvent;
	
	// Use this for pre-initialization
	private void Awake()
	{
		FlattenedGridList = new List<GameObject>();

        BuildGridState.BuildGridStateStartedEvent += OnBuildCubeStateStarted;
	}

    private void OnBuildCubeStateStarted()
    {
        CreateGrid();
    }

    private void CreateGrid()
	{
		grid = new GameObject[gridSize, gridSize, gridSize];
		
		int i = 0;
		
		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int z = 0; z < gridSize; z++)
				{
					GameObject tile = CreateTile(x, y, z, i);

					grid[x, y, z] = tile;

					FlattenedGridList.Add(tile);
					
					i++;
				}
			}
		}

		if (GridBuildFinishedEvent != null)
		{
			GridBuildFinishedEventArgs args = new GridBuildFinishedEventArgs();

			GridBuildFinishedEvent(this, args);
		}

		// TODO: do some fancy animations before calling BuilderReady
		BuilderReady();
	}
	
	private GameObject CreateTile(int x, int y, int z, int i)
	{
		// Instantiate tilePrefab
		GameObject tile = Instantiate(tilePrefab);
					
		// Get the tile's renderer
		Renderer tileRenderer = tile.GetComponent<Renderer>();
		
		// Setup position and make sure center of the grid is position 0, 0, 0
		Vector3 position = new Vector3(
			x - (tileRenderer.bounds.size.x + spaceBetweenTiles) * (gridSize / 2) + spaceBetweenTiles * x,
			y - (tileRenderer.bounds.size.y + spaceBetweenTiles) * (gridSize / 2) + spaceBetweenTiles * y,
			z - (tileRenderer.bounds.size.z + spaceBetweenTiles) * (gridSize / 2) + spaceBetweenTiles * z
		);
		
		tile.transform.position = position;
		
		tile.transform.parent = this.transform;
		
		tile.name = "tile_" + i;
		
		return tile;
	}
    
    private void BuilderReady()
    {
        BuilderReadyEventArgs args = new BuilderReadyEventArgs();
        
        if (BuilderReadyEvent != null)
        {
            BuilderReadyEvent(this, args);
        }
    }

	public void ClearGrid()
	{
		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int z = 0; z < gridSize; z++)
				{
					grid[x, y, z] = null;
				}
			}
		}

		FlattenedGridList.Clear();
	}
}
