﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	[SerializeField] private GameStateController gameStateController;

	[SerializeField] private UIView[] uiViews;

	private UIViewID currentViewID = UIViewID.None;

	private UIViewID nextViewID = UIViewID.None;

	private bool isSwappingViews = false;

	private void Awake()
	{
		// TODO: Create a class which can determine which UI to show based on game state enter/exit

		gameStateController.GetGameState<MainMenuState>().StateStartedEvent += OnMainMenuStateStarted;

		gameStateController.GetGameState<SetupGameState>().StateStartedEvent += OnSetupGameStateStarted;

		gameStateController.GetGameState<LevelWonState>().StateStartedEvent += OnLevelWonStateStarted;

		gameStateController.GetGameState<GameOverState>().StateStartedEvent += OnGameOverStateStarted;

		InitializeAllViews();
	}

    private void OnMainMenuStateStarted(object sender, StateStartedArgs e)
	{
		SwitchView(UIViewID.Main);
	}

	private void OnSetupGameStateStarted(object sender, StateStartedArgs e)
	{
		SwitchView(UIViewID.InGame);
	}

    private void OnLevelWonStateStarted(object sender, StateStartedArgs e)
    {
        HideCurrentView();
    }

	private void OnGameOverStateStarted(object sender, StateStartedArgs e)
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

			uiView.ShowCompleteEvent += OnShowComplete;

			uiView.HideCompleteEvent += OnHideComplete;

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
	}

	private void HideView(UIViewID uiViewID)
	{
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

	private void OnShowComplete(UIView uIView)
    {
        currentViewID = uIView.UIViewID;
    }

    private void OnHideComplete(UIView uIView)
    {
		if (isSwappingViews)
		{
        	ShowView(nextViewID);

			nextViewID = UIViewID.None;

			isSwappingViews = false;
		}

		currentViewID = UIViewID.None;
    }

	private void HideCurrentView()
	{
		HideView(currentViewID);
	}

	private void SwitchView(UIViewID uiViewID)
	{
		if (currentViewID == UIViewID.None
			|| currentViewID == uiViewID)
		{
			ShowView(uiViewID);

			return;
		}

		nextViewID = uiViewID;

		isSwappingViews = true;

		HideCurrentView();
	}

	public T GetView<T>() where T : UIView
	{
		for (int i = 0; i < uiViews.Length; i++)
		{
			Type type = uiViews[i].GetType();

			if (type == typeof(T))
			{
				return uiViews[i] as T;
			}
		}

		Debug.LogError("Unable to find UI View of type: " + typeof(T).Name);

		return null;
	}
}
