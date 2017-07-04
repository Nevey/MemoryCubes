using UnityEngine;
using System;

public class BuilderReadyEventArgs : EventArgs
{
    
}

public class Builder : MonoBehaviour 
{
	private GameObject[,,] grid;
    
    private int tilesInitializingCount = 0;
	
	public int gridSize = 3;
	
	public GameObject tilePrefab;
	
	public float spaceBetweenTiles = 0.2f;
    
    public event EventHandler<BuilderReadyEventArgs> BuilderReadyEvent;
	
	// Use this for pre-initialization
	private void Awake() 
	{
        BuildCubeState.BuildCubeStateStartedEvent += OnBuildCubeStateStarted;
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
                    tilesInitializingCount++;
                    
					grid[x, y, z] = CreateTile(x, y, z, i);
					
					i++;
				}
			}
		}
	}
	
	private GameObject CreateTile(int x, int y, int z, int i)
	{
		// Instantiate tilePrefab
		GameObject tile = Instantiate(tilePrefab);
        
        tile.GetComponent<ColorChanger>().ColorChangeReadyEvent += OnColorChangeReadyEvent;
					
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
    
    private void OnColorChangeReadyEvent(object sender, ColorChangeReadyEventArgs e)
    {
        tilesInitializingCount--;
        
        if (tilesInitializingCount == 0)
        {
            BuilderReady();
        }
        
        e.cube.GetComponent<ColorChanger>().ColorChangeReadyEvent -= OnColorChangeReadyEvent;
    }
    
    private void BuilderReady()
    {
        BuilderReadyEventArgs args = new BuilderReadyEventArgs();
        
        if (BuilderReadyEvent != null)
        {
            BuilderReadyEvent(this, args);
        }
    }
}
