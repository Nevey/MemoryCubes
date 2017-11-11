using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private GameModeConfig gameModeConfig;

    [SerializeField] private GameModeController gameModeController;

    [SerializeField] private ButtonGameModeCorresponder[] buttonGameModeCorresponders;

    public static event Action GameModePressedEvent;

    private void OnEnable()
    {
        for (int i = 0; i < buttonGameModeCorresponders.Length; i++)
        {
            // Add click event for button
            ButtonGameModeCorresponder buttonGameModeCorresponder = buttonGameModeCorresponders[i];

            buttonGameModeCorresponder.Button.onClick.AddListener(() => 
            {
                gameModeController.SetGameMode(buttonGameModeCorresponder.CorrespindingGameMode);

                DispatchGameModePressed();
            });

            // Find a corresponding game mode for the button
            bool correspondingGameModeFound = false;

            for (int k = 0; k < gameModeConfig.GameModes.Length; k++)
            {
                GameMode gameMode = gameModeConfig.GameModes[k];

                correspondingGameModeFound = buttonGameModeCorresponder.IsGameMode(gameMode);

                if (correspondingGameModeFound)
                {
                    break;
                }
            }

            // Enable/Disable button
            buttonGameModeCorresponder.gameObject.SetActive(correspondingGameModeFound);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < buttonGameModeCorresponders.Length; i++)
        {
            buttonGameModeCorresponders[i].Button.onClick.RemoveListener(DispatchGameModePressed);
        }
    }

    private void DispatchGameModePressed()
    {
        if (GameModePressedEvent != null)
        {
            GameModePressedEvent();
        }
    }
}