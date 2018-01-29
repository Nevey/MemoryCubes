using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] private UIView[] uiViews;

	private UIViewID currentViewID = UIViewID.None;

	private UIViewID nextViewID = UIViewID.None;

	private bool isSwappingViews = false;

	private void Awake()
	{
		// TODO: Create a class which can determine which UI to show based on game state enter
		MainMenuState.MainMenuStateStartedEvent += OnMainMenuStateStarted;

		SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;

		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;

		InitializeAllViews();
	}

	private void OnMainMenuStateStarted()
	{
		SwitchView(UIViewID.Main);
	}

	private void OnSetupGameStateStarted()
	{
		SwitchView(UIViewID.InGame);
	}

	private void OnGameOverStateStarted()
	{
		SwitchView(UIViewID.GameOver);
	}

	private void InitializeAllViews()
	{
		foreach (UIViewID uiViewID in Enum.GetValues(typeof(UIViewID)))
		{
			InitializeView(uiViewID);
		}
	}

	private void InitializeView(UIViewID uiViewID)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			uiView.AnimateInFinishedEvent += OnAnimateInFinished;

			uiView.AnimateOutFinishedEvent += OnAnimateOutFinished;

			if (uiView.UIViewID == uiViewID)
			{
				uiView.Initialize();
			}
		}
	}

    private void ShowView(UIViewID uiViewID)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewID == uiViewID)
			{
				uiView.Show();
			}
		}

		currentViewID = uiViewID;
	}

	private void HideView(UIViewID uiViewID)
	{
		currentViewID = UIViewID.None;
		
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewID == uiViewID)
			{
				uiView.Hide();

				break;
			}
		}
	}

	private void OnAnimateInFinished()
    {
        
    }

    private void OnAnimateOutFinished()
    {
		if (isSwappingViews)
		{
        	ShowView(nextViewID);

			nextViewID = UIViewID.None;

			isSwappingViews = false;
		}
    }

	private void HideCurrentView()
	{
		HideView(currentViewID);
	}

	private void SwitchView(UIViewID uiViewID)
	{
		if (currentViewID == UIViewID.None)
		{
			ShowView(uiViewID);

			return;
		}

		nextViewID = uiViewID;

		isSwappingViews = true;

		HideCurrentView();
	}
}
