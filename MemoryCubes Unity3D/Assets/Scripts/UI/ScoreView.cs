using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
	[SerializeField] private Text scoreText;

	[SerializeField] private ScoreController scoreController;

	public void UpdateScoreText()
	{
		scoreText.text = scoreController.CurrentScore.ToString();
	}
}
