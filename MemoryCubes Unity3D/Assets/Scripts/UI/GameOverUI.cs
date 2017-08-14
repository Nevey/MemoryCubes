using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private AnimationCurve showCurve;

	private float currentTime;

	private bool isActive;

	// Use this for initialization
	private void Awake()
	{
		// No need to show game over image on init...
		GetComponent<Image>().enabled = false;
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

		float position = showCurve.Evaluate(currentTime);
	}
		
	private void OnGameOverStateStarted()
	{
		ShowGameOver();
	}

	private void ShowGameOver()
	{
		GetComponent<Image>().enabled = true;

		isActive = true;

		currentTime = 0f;
	}
}
