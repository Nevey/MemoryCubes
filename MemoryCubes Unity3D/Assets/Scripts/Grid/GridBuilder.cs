using UnityEngine;
using System;
using System.Collections.Generic;
using UnityTools.Base;

// TODO: Move to own file...
public class GridBuildFinishedEventArgs : EventArgs
{

}

public class GridBuilder : MonoBehaviourSingleton<GridBuilder>
{
	[SerializeField] private float spaceBetweenTiles = 0.2f;

	[SerializeField] private float tileScaleTweak = 5f;
	
	[SerializeField] private Transform parent;

	[SerializeField] private GameObject tilePrefab;

	[SerializeField] private GridBuilderAnimator gridBuilderAnimator;

	[SerializeField] private ParticlesSpawner particlesSpawner;

	[SerializeField] private GridConfig gridConfig;

	[SerializeField] private RoutineUtility routineUtility;

	private int gridSize;

	private GameObject[,,] grid;

	private List<GameObject> flattenedGridList;

	public int GridSize { get { return gridSize; } }

	public GameObject[,,] Grid { get { return grid; } }

	public List<GameObject> FlattenedGridList {	get { return flattenedGridList; } }
    
	public event EventHandler<GridBuildFinishedEventArgs> GridBuildFinishedEvent;

    public event Action BuilderReadyEvent;
	
	// Use this for pre-initialization
	private void Awake()
	{
		flattenedGridList = new List<GameObject>();
		
		GameStateMachine.Instance.GetState<BuildCubeState>().StartEvent += OnBuildCubeStateStarted;
	}

    private void OnEnable()
	{
		// TODO: Don't wait for animation, clear grid on game over state enter instead
		UIController.Instance.GetView<GameOverView>().ShowCompleteEvent += OnGameOverShowComplete;
	}

    private void OnDisable()
	{
		UIController.Instance.GetView<GameOverView>().ShowCompleteEvent -= OnGameOverShowComplete;
	}

    private void OnBuildCubeStateStarted()
    {
        CreateGrid();
    }

    private void OnGameOverShowComplete(UIView obj)
    {
        ClearGrid();
    }

    private void CreateGrid()
	{
		// Get the grid size based on current level, value is determined in
		// an animation curve from the grid config
		gridSize = (int)gridConfig.GridSizeCurve.Evaluate(LevelController.Instance.CurrentLevel);

		// Create new game object array
		grid = new GameObject[gridSize, gridSize, gridSize];
		
		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int z = 0; z < gridSize; z++)
				{
					// TODO: Create Tile / CubeTile / GridTile class and keep track of data
					// such as grid coordinates there
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

		// Do some animations, when done: building is ready!
		gridBuilderAnimator.AnimateTiles(flattenedGridList, BuilderReady);
	}
	
	private GameObject CreateTile(int x, int y, int z)
	{
		// Instantiate tilePrefab
		GameObject tile = Instantiate(tilePrefab);

		tile.GetComponent<GridCoordinates>().SetGridPosition(x, y, z);

		float scaledSpaceBetweenTiles = spaceBetweenTiles / gridSize;

		// Scale the tile based on grid size to make the grid fit the camera
		float tileScale = tileScaleTweak / (gridSize + (scaledSpaceBetweenTiles * gridSize));

		tile.GetComponent<Resizer>().SetOriginScale(tileScale);

		tile.transform.localScale = new Vector3(
			tileScale,
			tileScale,
			tileScale
		);

		// Set position based on tile scale
		float halfGridSize = (tileScale + scaledSpaceBetweenTiles) * gridSize / 2;

		float halfTileScale = (tileScale + scaledSpaceBetweenTiles) / 2;

		Vector3 position = new Vector3(
			(tileScale + scaledSpaceBetweenTiles) * x - halfGridSize + halfTileScale,
			(tileScale + scaledSpaceBetweenTiles) * y - halfGridSize + halfTileScale,
			(tileScale + scaledSpaceBetweenTiles) * z - halfGridSize + halfTileScale
		);
		
		tile.transform.position = position;
		
		tile.transform.parent = parent;
		
		tile.name = "tile [" + x + ", " + y + ", " + z + "]";
		
		return tile;
	}

    private void BuilderReady()
    {
		if (BuilderReadyEvent != null)
		{
			BuilderReadyEvent();
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

	private void DestroyTileDelayed(GameObject tile, float delay, Action callback = null)
	{
		routineUtility.StartWaitTimeRoutine(delay, () =>
		{
			DestroyTile(tile);

			if (callback != null)
			{
				callback();
			}
		});
	}

	private int GetAliveTileCount()
	{
		int aliveTileCount = 0;

		for (int i = 0; i < flattenedGridList.Count; i++)
		{
			if (flattenedGridList[i] != null)
			{
				aliveTileCount++;
			}
		}

		return aliveTileCount;
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

	public void ClearGridDelayed(float delayPerTile, Action<bool> callback = null)
	{
		int aliveTileCount = GetAliveTileCount();

		int currentTileCount = 0;

		int index = 1;

		for (int x = 0; x < gridSize; x++)
		{
			for (int y = 0; y < gridSize; y++)
			{
				for (int z = 0; z < gridSize; z++)
				{
					GameObject tile = grid[x, y, z];

					if (tile == null)
					{
						continue;
					}

					float delay = delayPerTile * index;

					DestroyTileDelayed(tile, delay, () =>
					{
						if (callback != null)
						{
							currentTileCount++;

							callback(currentTileCount == aliveTileCount);
						}
					});

					grid[x, y, z] = null;

					index++;
				}
			}
		}
	}
}
