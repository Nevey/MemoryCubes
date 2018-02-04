using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Base;

public class ScoreController : MonoBehaviourSingleton<ScoreController>
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
		// gameStateController.GetGameState<GameOverState>().StateStartedEvent += OnGameOverStateStarted;

	}

    private void OnDisable()
	{
		// gameStateController.GetGameState<GameOverState>().StateStartedEvent -= OnGameOverStateStarted;
	}

	private void OnGameOverStateStarted(object sender, StateStartedArgs e)
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

	private void AddScore(int score)
	{
		// Give bonus for cube count
		currentScore += score * LevelController.Instance.CurrentCubeCount;

		scoreView.UpdateScoreText();

		// TODO: Show separate score floaters for tile score and bonus score
		scoreView.ShowScoreFloater(score);
	}

	/// <summary>
	/// Add score based on collected tile amount
	/// </summary>
	/// <param name="collectedTileAmount"></param>
	public void AddBulkScore(int collectedTileAmount)
	{
		int tileScore = scoreConfig.ScorePerTile * collectedTileAmount;

		int bonusScore = scoreConfig.TileScoreMultiplier * collectedTileAmount;

		AddScore(tileScore + bonusScore);
	}

	/// <summary>
	/// Apply a penalty, when collecting a cube that's not the same color
	/// as the target color
	/// </summary>
	public void ApplyPenalty()
	{
		AddScore(scoreConfig.PenaltyPerTile);
	}

	/// <summary>
	/// Add score per tile for last tiles standing
	/// </summary>
	public void AddLastStandingTileScore()
	{
		AddScore(scoreConfig.ScorePerLastStandingTile);
	}
}
