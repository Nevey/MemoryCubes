using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeConfig : MonoBehaviour
{
	[SerializeField] private float maxTime = 5f;

    [SerializeField] private float bonusTimePerTile = 0.1f;

    [SerializeField] private float penaltyPerTile = 0.3f;

	[SerializeField] private float penaltyNoSelectedTiles = 0.3f;

	public float MaxTime { get { return maxTime; } }

	public float BonusTimePerTile { get { return bonusTimePerTile; } }

	public float PenaltyPerTile { get { return penaltyPerTile; } }

	public float PenaltyNoSelectedTiles { get { return penaltyNoSelectedTiles; } }
}
