using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreConfig : MonoBehaviour
{
	[SerializeField] private int scorePerTile;

	[SerializeField] private float tileScoreMultiplier;

	[SerializeField] private int penaltyPerTile;	

	public int ScorePerTile { get	{ return scorePerTile; } }

	public float TileScoreMultiplier { get { return tileScoreMultiplier; } }

	public int PenaltyPerTile { get { return penaltyPerTile; } }
}
