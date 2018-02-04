using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeTileChecker : MonoBehaviour
{
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
			|| gridPosition.x == GridBuilder.Instance.GridSize - 1)
		{
			return true;
		}

		if (gridPosition.y == 0
			|| gridPosition.y == GridBuilder.Instance.GridSize - 1)
		{
			return true;
		}

		if (gridPosition.z == 0
			|| gridPosition.z == GridBuilder.Instance.GridSize - 1)
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

		if (GridBuilder.Instance.Grid[x - 1, y, z] != null
			&& GridBuilder.Instance.Grid[x + 1, y, z] != null
			&& GridBuilder.Instance.Grid[x, y - 1, z] != null
			&& GridBuilder.Instance.Grid[x, y + 1, z] != null
			&& GridBuilder.Instance.Grid[x, y, z - 1] != null
			&& GridBuilder.Instance.Grid[x, y, z + 1] != null)
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
