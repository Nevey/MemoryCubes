using UnityEngine;
using System;
using System.Collections.Generic;

public class GridBuildFinishedEventArgs : EventArgs
{

}

public class BuilderReadyEventArgs : EventArgs
{
    
}

public class GridBuilder : MonoBehaviour 
{
	[SerializeField] private float spaceBetweenTiles = 0.2f;

	[SerializeField] private float tileScaleTweak = 5f;
	
	[SerializeField] private Transform parent;

	[SerializeField] private GameObject tilePrefab;

	[SerializeField] private GameOverView gameOverView;

	[SerializeField] private LevelController levelController;

	[SerializeField] private ParticlesSpawner particlesSpawner;

	[SerializeField] private GridConfig gridConfig;

	private int gridSize;

	private GameObject[,,] grid;

	private List<GameObject> flattenedGridList;

	public int GridSize { get { return gridSize; } }

	public GameObject[,,] Grid { get { return grid; } }

	public List<GameObject> FlattenedGridList {	get { return flattenedGridList; } }
    
	public event EventHandler<GridBuildFinishedEventArgs> GridBuildFinishedEvent;

    public static event EventHandler<BuilderReadyEventArgs> BuilderReadyEvent;
	
	// Use this for pre-initialization
	private void Awake()
	{
		flattenedGridList = new List<GameObject>();

        BuildGridState.BuildGridStateStartedEvent += OnBuildCubeStateStarted;
	}

	private void OnEnable()
	{
		gameOverView.GameOverShowFinishedEvent += OnGameOverShowFinished;
	}

	private void OnDisable()
	{
		gameOverView.GameOverShowFinishedEvent -= OnGameOverShowFinished;
	}

    private void OnBuildCubeStateStarted()
    {
        CreateGrid();
    }

	 private void OnGameOverShowFinished()
    {
        ClearGrid();
    }

    private void CreateGrid()
	{
		// Get the grid size based on current level, value is determined in
		// an animation curve from the grid config
		gridSize = (int)gridConfig.GridSizeCurve.Evaluate(levelController.CurrentLevel);

		// Create new game object array
		grid = new GameObject[gridSize, gridSize, gridSize];
		
		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int z = 0; z < gridSize; z++)
				{
					GameObject tile = CreateTile(x, y, z);

					grid[x, y, z] = tile;

					flattenedGridList.Add(tile);
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
	
	private GameObject CreateTile(int x, int y, int z)
	{
		// Instantiate tilePrefab
		GameObject tile = Instantiate(tilePrefab);

		tile.GetComponent<GridCoordinates>().SetGridPosition(x, y, z);

		// Scale the tile based on grid size to make the grid fit the camera
		float tileScale = tileScaleTweak / (gridSize + (spaceBetweenTiles * gridSize));

		tile.transform.localScale = new Vector3(
			tileScale,
			tileScale,
			tileScale
		);

		// Set position based on tile scale
		float halfGridSize = (tileScale + spaceBetweenTiles) * gridSize / 2;

		float halfTileScale = (tileScale + spaceBetweenTiles) / 2;

		Vector3 position = new Vector3(
			(tileScale + spaceBetweenTiles) * x - halfGridSize + halfTileScale,
			(tileScale + spaceBetweenTiles) * y - halfGridSize + halfTileScale,
			(tileScale + spaceBetweenTiles) * z - halfGridSize + halfTileScale
		);
		
		tile.transform.position = position;
		
		tile.transform.parent = parent;
		
		tile.name = "tile [" + x + ", " + y + ", " + z + "]";
		
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

	private void DestroyTile(GameObject tile)
	{
		if (flattenedGridList.Contains(tile))
		{
			flattenedGridList.Remove(tile);
		}

		// Check if tile was already destroyed
		if (tile == null)
		{
			return;
		}

		particlesSpawner.Spawn(
            tile.transform,
            tile.GetComponent<TileColor>().MyColor
        );

		Destroyer destroyer = tile.GetComponent<Destroyer>();

		destroyer.DestroyCube();
	}

	public void ClearTile(GameObject tile)
	{
		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int z = 0; z < gridSize; z++)
				{
					if (grid[x, y, z] == tile)
					{
						DestroyTile(grid[x, y, z]);

						grid[x, y, z] = null;

						return;
					}
				}
			}
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
					DestroyTile(grid[x, y, z]);

					grid[x, y, z] = null;
				}
			}
		}
	}
}
