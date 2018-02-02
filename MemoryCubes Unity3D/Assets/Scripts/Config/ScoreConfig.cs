using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreConfig : MonoBehaviour
{
	[SerializeField] private int scorePerTile;

	[SerializeField] private int scorePerLastStandingTile;

	[SerializeField] private int tileScoreMultiplier;

	[SerializeField] private int penaltyPerTile;	

	public int ScorePerTile { get { return scorePerTile; } }

	public int ScorePerLastStandingTile { get { return scorePerLastStandingTile; } }

	public int TileScoreMultiplier { get { return tileScoreMultiplier; } }

	public int PenaltyPerTile { get { return penaltyPerTile; } }
}
