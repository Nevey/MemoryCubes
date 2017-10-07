using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button startButton;
    
	public static event Action StartPressedEvent;

    private void OnEnable()
    {
        startButton.onClick.AddListener(DispatchStartPressed);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(DispatchStartPressed);
    }

    private void DispatchStartPressed()
    {
        if (StartPressedEvent != null)
        {
            StartPressedEvent();
        }
    }
}