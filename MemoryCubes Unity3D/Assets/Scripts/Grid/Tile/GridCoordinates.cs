using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCoordinates : MonoBehaviour
{
	private Vector3 gridPosition;

	public Vector3 GridPosition { get { return gridPosition; } }

	/// <summary>
	/// Set grid coordinates
	/// </summary>
	/// <param name="value"></param>
	public void SetGridPosition(Vector3 value)
	{
		gridPosition = value;
	}

	/// <summary>
	/// Set grid coordinates
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="z"></param>
	public void SetGridPosition(int x, int y, int z)
	{
		gridPosition = new Vector3(x, y, z);
	}
}
