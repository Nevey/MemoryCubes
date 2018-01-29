using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : UIView
{
	[SerializeField] private ScoreController scoreController;

	[SerializeField] private LevelController levelController;

	[SerializeField] private Text cubesClearedText;

	[SerializeField] private Text scoreText;

	[SerializeField] private Text highScoreText;

	[SerializeField] private Image exclamation;

	private void Update()
	{
		if (isEnabled)
		{
			if (Input.GetMouseButton(0))
			{
				Hide();
			}
		}
	}

	private void SetupTexts()
	{
		// Don't check current cube count, but current level
		// instead, as the player did not finish the final cube he/she reached!
		cubesClearedText.text = levelController.CurrentLevel.ToString();

		scoreText.text = scoreController.LastScore.ToString();

		highScoreText.text = scoreController.HighScore.ToString();
	}

	private void SetupHighscoreIndicator()
	{
		exclamation.gameObject.SetActive(IsNewHighScore());
	}

	/// <summary>
	/// Don't do this in score controller for ordering reasons (TO FIX)
	/// </summary>
	/// <returns></returns>
	private bool IsNewHighScore()
	{
		return scoreController.LastScore == scoreController.HighScore;
	}

	public override void Show()
	{
		base.Show();

		SetupTexts();
		
		SetupHighscoreIndicator();
	}
}
