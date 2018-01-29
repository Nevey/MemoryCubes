using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIView
{
    [SerializeField] private GameModeConfig gameModeConfig;

    [SerializeField] private GameModeController gameModeController;

    [SerializeField] private ButtonGameModeCorresponder[] buttonGameModeCorresponders;

    private void EnableButtons()
    {
        for (int i = 0; i < buttonGameModeCorresponders.Length; i++)
        {
            ButtonGameModeCorresponder buttonGameModeCorresponder = buttonGameModeCorresponders[i];

            SetButtonListener(buttonGameModeCorresponder);

            SetButtonActive(buttonGameModeCorresponder);
        }
    }

    private void DisableButtons()
    {
        for (int i = 0; i < buttonGameModeCorresponders.Length; i++)
        {
            buttonGameModeCorresponders[i].Button.onClick.RemoveAllListeners();
        }
    }

    private void SetButtonListener(ButtonGameModeCorresponder buttonGameModeCorresponder)
    {
        buttonGameModeCorresponder.Button.onClick.AddListener(() => 
        {
            gameModeController.SetGameMode(buttonGameModeCorresponder.CorrespindingGameMode);

            Hide();
        });
    }

    private void SetButtonActive(ButtonGameModeCorresponder buttonGameModeCorresponder)
    {
        for (int i = 0; i < gameModeConfig.GameModes.Length; i++)
        {
            GameMode gameMode = gameModeConfig.GameModes[i];

            // Corresponding game mode for this button found! Enable game button!
            if (buttonGameModeCorresponder.IsGameMode(gameMode))
            {
                buttonGameModeCorresponder.gameObject.SetActive(true);

                break;
            }
        }
    }

    public override void Show()
    {
        base.Show();

        EnableButtons();
    }

    public override void Hide()
    {
        base.Hide();

        DisableButtons();
    }
}