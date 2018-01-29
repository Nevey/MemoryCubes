using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour 
{
	[SerializeField] private ScoreConfig scoreConfig;

	[SerializeField] private ScoreView scoreView;

	private const string highScoreKey = "highScore";

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

	/// <summary>
	/// Get the user's highscore
	/// </summary>
	/// <returns></returns>
	public int HighScore { get { return LoadHighScore(); } }

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

		if (LoadHighScore() < lastScore)
		{
			SaveHighScore(lastScore);
		}
	}

	private void ResetScore()
	{
		currentScore = 0;

		scoreView.UpdateScoreText();
	}

	private void SaveHighScore(int score)
	{
		PlayerPrefs.SetInt(highScoreKey, score);
	}

	private int LoadHighScore()
	{
		return PlayerPrefs.GetInt(highScoreKey);
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

		scoreView.UpdateScoreText();

		// TODO: Show separate score floaters for tile score and bonus score
		scoreView.ShowScoreFloater(addedScore);
	}

	/// <summary>
	/// Apply a penalty, when collecting a cube that's not the same color
	/// as the target color
	/// </summary>
	public void ApplyPenalty()
	{
		int penaltyValue = scoreConfig.PenaltyPerTile;

		currentScore -= penaltyValue;

		scoreView.UpdateScoreText();

		scoreView.ShowScoreFloater(penaltyValue);
	}
}
