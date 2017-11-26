using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] private UIView[] uiViews;

	private UIViewType currentViewType;

	private void Awake()
	{
		MainMenuState.MainMenuStateStartedEvent += OnMainMenuStateStarted;

		SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;

		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;

		InitializeAllViews();
	}

	private void OnMainMenuStateStarted()
	{
		SwitchView(UIViewType.Main);
	}

	private void OnSetupGameStateStarted()
	{
		SwitchView(UIViewType.InGame);
	}

	private void OnGameOverStateStarted()
	{
		SwitchView(UIViewType.GameOver);
	}

	private void InitializeAllViews()
	{
		foreach (UIViewType uiViewType in Enum.GetValues(typeof(UIViewType)))
		{
			InitializeView(uiViewType);
		}
	}

	private void InitializeView(UIViewType uiViewType)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewType == uiViewType)
			{
				uiView.Initialize();
			}
		}
	}

	private void ShowView(UIViewType uiViewType)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewType == uiViewType)
			{
				uiView.Show();
			}
		}

		currentViewType = uiViewType;
	}

	private void HideView(UIViewType uiViewType)
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			UIView uiView = uiViews[i];

			if (uiView.UIViewType == uiViewType)
			{
				uiView.Hide();
			}
		}
	}

	private void HideCurrentView()
	{
		HideView(currentViewType);
	}

	private void SwitchView(UIViewType uiViewType)
	{
		HideCurrentView();

		ShowView(uiViewType);
	}
}
