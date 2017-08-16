﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectView : MonoBehaviour
{
	[SerializeField] private Button collectButton;

	[SerializeField] private CollectController collectController;

	private void OnEnable()
	{
		SetupGameState.SetupGameStateStartedEvent += OnSetupGameStateStarted;

		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;
	}

	private void OnDisable()
	{
		SetupGameState.SetupGameStateStartedEvent -= OnSetupGameStateStarted;

		GameOverState.GameOverStateStartedEvent -= OnGameOverStateStarted;
	}

	private void OnSetupGameStateStarted()
	{
		collectButton.enabled = true;

		collectButton.onClick.AddListener(collectController.CollectSelectedTiles);
	}

	private void OnGameOverStateStarted()
	{
		collectButton.enabled = false;

		collectButton.onClick.RemoveListener(collectController.CollectSelectedTiles);
	}
}