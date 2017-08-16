using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour 
{
	[SerializeField] private ScoreConfig scoreConfig;

	[SerializeField] private ScoreView scoreView;

	private int currentScore;

	public int CurrentScore { get { return currentScore; } }

	private void OnEnable()
	{
		SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;
	}

	private void OnDisable()
	{
		SetupGameState.SetupGameStateStartedEvent -= OnSetupGameStateStarted;
	}

	private void OnSetupGameStateStarted()
	{
		ResetScore();
	}

	private void ResetScore()
	{
		currentScore = 0;

		UpdateScoreView();
	}

	private void UpdateScoreView()
	{
		scoreView.UpdateScoreText();
	}

	/// <summary>
	/// Add score based on collected tile amount
	/// </summary>
	/// <param name="collectedTileAmount"></param>
	public void AddScore(int collectedTileAmount)
	{
		int bonusScore = (int)Mathf.Round(scoreConfig.TileScoreMultiplier * collectedTileAmount);

		int addedScore = scoreConfig.ScorePerTile + bonusScore;

		currentScore += addedScore;

		UpdateScoreView();
	}

	/// <summary>
	/// Apply a penalty, when collecting a cube that's not the same color
	/// as the target color for example...
	/// </summary>
	public void ApplyPenalty()
	{
		currentScore -= scoreConfig.PenaltyPerTile;

		UpdateScoreView();
	}
}
