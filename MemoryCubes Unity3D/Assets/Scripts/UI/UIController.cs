using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] private UIView[] uiViews;

	private UIViewID currentViewType;

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
		foreach (UIViewID uiViewType in Enum.GetValues(typeof(UIViewID)))
		{
			InitializeView(uiViewType);
		}
	}

	private void InitializeView(UIViewID uiViewType)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewID == uiViewType)
			{
				uiView.Initialize();
			}
		}
	}

	private void ShowView(UIViewID uiViewType)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewID == uiViewType)
			{
				uiView.Show();
			}
		}

		currentViewType = uiViewType;
	}

	private void HideView(UIViewID uiViewType)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewID == uiViewType)
			{
				uiView.Hide();
			}
		}
	}

	private void HideCurrentView()
	{
		HideView(currentViewType);
	}

	private void SwitchView(UIViewID uiViewType)
	{
		HideCurrentView();

		ShowView(uiViewType);
	}
}
