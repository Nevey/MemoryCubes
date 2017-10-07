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
		HideAllViews();
	}
	
	// Use this for initialization
	private void Start()
	{
		MainMenuState.MainMenuStateStartedEvent += OnMainMenuStateStarted;

		SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;
	}

	private void OnMainMenuStateStarted()
	{
		SwitchView(UIViewType.Main);
	}

	private void OnSetupGameStateStarted()
	{
		SwitchView(UIViewType.InGame);
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

	private void HideAllViews()
	{
		foreach (UIViewType uiViewType in Enum.GetValues(typeof(UIViewType)))
		{
			HideView(uiViewType);
		}
	}

	private void SwitchView(UIViewType uiViewType)
	{
		HideCurrentView();

		ShowView(uiViewType);
	}
}
