using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
	[SerializeField] private ScoreController scoreController;

	[SerializeField] private Text scoreText;

	[SerializeField] private ScoreFloater scoreFloaterPrefab;

	[SerializeField] private Transform scoreFloaterParent;

	public void UpdateScoreText()
	{
		scoreText.text = scoreController.CurrentScore.ToString();
	}

	public void ShowScoreFloater(int scoreValue)
	{
		// create score floater
		// set color based on score value (in score floater script)

		ScoreFloater scoreFloater = Instantiate(scoreFloaterPrefab);

		scoreFloater.transform.parent = scoreFloaterParent;

		scoreFloater.transform.position = transform.position;

		scoreFloater.transform.localScale = new Vector3(1f, 1f, 1f);

		scoreFloater.Setup(scoreValue);
	}
}
