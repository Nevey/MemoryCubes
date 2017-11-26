using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IOnUIViewInitialize, IOnUIViewShow
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

	private float currentTime;

	public event Action GameOverShowFinishedEvent;

	public static event Action GameOverHideFinishedEvent;
	
	private void Update()
	{
		// TODO: Use tweens instead!
		switch (currentAnimation)
		{
			case CurrentAnimation.None:
				// Return if no animation is active
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

	private void ShowGameOver()
	{
		currentAnimation = CurrentAnimation.ShowGameOver;

		currentTime = 0f;
	}

	private void HideGameOver()
	{
		currentAnimation = CurrentAnimation.HideGameOver;

		currentTime = 0f;
	}

	private void UpdateShowGameOver()
	{
		// Set position based on curve
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
		// Set position based on curve
		rectTransform.anchoredPosition = GetValueFromAnimationCurve(hideCurve);

		// Check if we passed the max given time in the curve
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

    public void OnUIViewInitialize()
    {
        currentAnimation = CurrentAnimation.None;

		rectTransform = GetComponent<RectTransform>();
    }

    public void OnUIViewShow()
    {
        ShowGameOver();
    }
}
