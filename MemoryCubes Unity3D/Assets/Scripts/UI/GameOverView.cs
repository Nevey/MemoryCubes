using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : UIView
{
	[SerializeField] private ScoreController scoreController;

	[SerializeField] private Text scoreText;

	private bool canHide = false;

	public event Action GameOverShowFinishedEvent;

	// TODO: Make all screens accessible via ui controller and use generic show/hide events
	public static event Action GameOverHideFinishedEvent;

	private void Update()
	{
		if (canHide)
		{
			if (Input.GetMouseButton(0))
			{
				Hide();

				canHide = false;
			}
		}
	}

	protected override void OnShowComplete()
	{
		base.OnShowComplete();

		canHide = true;

		if (GameOverShowFinishedEvent != null)
		{
			GameOverShowFinishedEvent();
		}
	}

	protected override void OnHideComplete()
	{
		base.OnHideComplete();
		
		if (GameOverHideFinishedEvent != null)
		{
			GameOverHideFinishedEvent();
		}
	}

	public override void Show()
	{
		base.Show();

		scoreText.text = scoreController.LastScore.ToString();
	}
}
