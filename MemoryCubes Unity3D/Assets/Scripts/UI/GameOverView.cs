using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour
{
	[SerializeField] private AnimationCurve showCurve;

	[SerializeField] private AnimationCurve hideCurve;

	private enum CurrentAnimation
	{
		None,
		ShowGameOver,
		ShowGameOverFinished,
		HideGameOver
	}

	private CurrentAnimation currentAnimation = new CurrentAnimation();

	private RectTransform rectTransform;

	private Image image;

	private float currentTime;

	public event Action GameOverShowFinishedEvent;

	public static event Action GameOverHideFinishedEvent;

	// Use this for initialization
	private void Awake()
	{
		currentAnimation = CurrentAnimation.None;

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
		switch (currentAnimation)
		{
			case CurrentAnimation.None:
				return;

			case CurrentAnimation.ShowGameOver:
				UpdateShowGameOver();
				break;

			case CurrentAnimation.ShowGameOverFinished:
				WaitForInput();
				break;

			case CurrentAnimation.HideGameOver:
				UpdateHideGameOver();
				break;
		}

		if (currentAnimation == CurrentAnimation.ShowGameOver
			|| currentAnimation == CurrentAnimation.HideGameOver)
		{
			currentTime += Time.deltaTime;
		}
	}
		
	private void OnGameOverStateStarted()
	{
		ShowGameOver();
	}

	private void ShowGameOver()
	{
		currentAnimation = CurrentAnimation.ShowGameOver;

		currentTime = 0f;

		image.enabled = true;
	}

	private void HideGameOver()
	{
		currentAnimation = CurrentAnimation.HideGameOver;

		currentTime = 0f;
	}

	private void UpdateShowGameOver()
	{
		rectTransform.anchoredPosition = GetValueFromAnimationCurve(showCurve);

		// Check if we passed the max given time in the curve
		if (IsCurveFinished(showCurve))
		{
			currentAnimation = CurrentAnimation.ShowGameOverFinished;

			if (GameOverShowFinishedEvent != null)
			{
				GameOverShowFinishedEvent();
			}
		}
	}

	private void WaitForInput()
	{
		if (Input.GetMouseButton(0))
		{
			HideGameOver();
		}
	}
	
	private void UpdateHideGameOver()
	{
		rectTransform.anchoredPosition = GetValueFromAnimationCurve(hideCurve);

		if (IsCurveFinished(hideCurve))
		{
			currentAnimation = CurrentAnimation.None;

			if (GameOverHideFinishedEvent != null)
			{
				GameOverHideFinishedEvent();
			}
		}
	}

	private Vector2 GetValueFromAnimationCurve(AnimationCurve animationCurve)
	{
		float targetPosition = animationCurve.Evaluate(currentTime);

		Vector2 position = new Vector2(
			rectTransform.anchoredPosition.x,
			targetPosition
		);

		return position;
	}

	private bool IsCurveFinished(AnimationCurve animationCurve)
	{
		return currentTime > animationCurve.keys[animationCurve.keys.Length - 1].time;
	}
}
