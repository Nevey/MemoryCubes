using UnityEngine;

public class Builder : MonoBehaviour 
{
	private GameObject[,,] grid;
	
	public int gridSize = 3;
	
	public GameObject tilePrefab;
	
	public float spaceBetweenTiles = 0.2f;
	
	// Use this for initialization
	void Start() 
	{
		CreateGrid();
	}
	
	// Update is called once per frame
	void Update() 
	{
		
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
}
