using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour 
{
	[SerializeField] private ScoreConfig scoreConfig;

	[SerializeField] private ScoreView scoreView;

	private int currentScore;

	private int lastScore;

	/// <summary>
	/// Get the user's current score in the current session
	/// </summary>
	/// <returns></returns>
	public int CurrentScore { get { return currentScore; } }

	/// <summary>
	/// Get the user's score from the last game
	/// </summary>
	/// <returns></returns>
	public int LastScore { get { return lastScore; } }

	private void OnEnable()
	{
		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;
	}

	private void OnDisable()
	{
		GameOverState.GameOverStateStartedEvent -= OnGameOverStateStarted;
	}

	private void OnGameOverStateStarted()
	{
		StoreCurrentScore();

		ResetScore();
	}

	private void StoreCurrentScore()
	{
		lastScore = currentScore;
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
