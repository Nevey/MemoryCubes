using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFloater : MonoBehaviour
{
	[SerializeField] private Color positiveColor;

	[SerializeField] private Color negativeColor;

	[SerializeField] private Text scoreText;

	[SerializeField] private Vector2 relativeStartPosition;

	[SerializeField] private Vector2 relativeTargetPosition;

	[SerializeField] private float tweenTime = 1f;

	[SerializeField] private Ease tweenEase;

	private void DoTweens()
	{
		Vector3 targetPosition = new Vector3(
			transform.position.x + relativeTargetPosition.x,
			transform.position.y + relativeTargetPosition.y,
			transform.position.z
		);

		transform.DOMove(targetPosition, tweenTime)
			.SetEase(tweenEase)
			.OnComplete(() => { Destroy(gameObject); });

		// Fade in/out?
	}

	public void Setup(int scoreValue)
	{
		string scoreString = "+";

		Color textColor = positiveColor;

		if (scoreValue <= 0)
		{
			scoreString = "";

			textColor = negativeColor;
		}

		scoreString += scoreValue.ToString();

		scoreText.text = scoreString;

		scoreText.color = textColor;

		transform.position = new Vector3(
			transform.position.x + relativeStartPosition.x,
			transform.position.y + relativeStartPosition.y,
			transform.position.z
		);

		DoTweens();
	}
}
