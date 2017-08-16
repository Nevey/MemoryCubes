using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeTileChecker : MonoBehaviour
{
	[SerializeField] private GridBuilder gridBuilder;

	/// <summary>
	/// Checks if the current tile is completely surrounded by other tiles
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	/// <returns>bool</returns>
	private bool IsTileFree(Vector3 gridPosition)
	{
		if (IsTileOnEdgeOfGrid(gridPosition))
		{
			return true;
		}

		if (!IsTileSurrounded(gridPosition))
		{
			return true;
		}
		
		return false;
	}

	private bool IsTileOnEdgeOfGrid(Vector3 gridPosition)
	{
		if (gridPosition.x == 0
			|| gridPosition.x == gridBuilder.GridSize - 1)
		{
			return true;
		}

		if (gridPosition.y == 0
			|| gridPosition.y == gridBuilder.GridSize - 1)
		{
			return true;
		}

		if (gridPosition.z == 0
			|| gridPosition.z == gridBuilder.GridSize - 1)
		{
			return true;
		}

		return false;
	}

	private bool IsTileSurrounded(Vector3 gridPosition)
	{
		int x = (int)gridPosition.x;
		int y = (int)gridPosition.y;
		int z = (int)gridPosition.z;

		if (gridBuilder.Grid[x - 1, y, z] != null
			&& gridBuilder.Grid[x + 1, y, z] != null
			&& gridBuilder.Grid[x, y - 1, z] != null
			&& gridBuilder.Grid[x, y + 1, z] != null
			&& gridBuilder.Grid[x, y, z - 1] != null
			&& gridBuilder.Grid[x, y, z + 1] != null)
		{
			return true;
		}

		return false;
	}

	public bool CanTapTile(Vector3 gridPosition)
	{
		return IsTileFree(gridPosition);
	}
}
