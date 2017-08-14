using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
	[SerializeField] private AnimationCurve showCurve;

	private RectTransform rectTransform;

	private Image image;

	private float currentTime;

	private bool isActive;

	// Use this for initialization
	private void Awake()
	{
		// No need to show game over image on init...
		image = GetComponent<Image>();

		image.enabled = false;

		rectTransform = GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;
	}

	private void OnDisable()
	{
		GameOverState.GameOverStateStartedEvent -= OnGameOverStateStarted;
	}
	
	// Update is called once per frame
	private void Update()
	{
		if (!isActive)
		{
			return;
		}

		currentTime += Time.deltaTime;

		float targetPosition = showCurve.Evaluate(currentTime);

		Vector2 position = new Vector2(
			rectTransform.anchoredPosition.x,
			targetPosition
		);

		rectTransform.anchoredPosition = position;
	}
		
	private void OnGameOverStateStarted()
	{
		ShowGameOver();
	}

	private void ShowGameOver()
	{
		image.enabled = true;

		isActive = true;

		currentTime = 0f;
	}
}
