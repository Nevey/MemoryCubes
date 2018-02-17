using System;
using UnityEngine;

// Rename to MaterialColorizer
public class MaterialColorizer : MonoBehaviour
{
    [Header("Generic Color Change Settings")]
    [SerializeField] private Material targetMaterial;

    [SerializeField] private ColorConfig colorConfig;

    [SerializeField] private float colorChangeTime = 0.3f;

    [Header("Main Menu Settings")]
    [SerializeField] private Color startColor = Color.white;

    [SerializeField] private float timeUntilColorChange = 5f;

    private Deadzone deadzone = new Deadzone();

    private Color targetColor;

    private Color currentColor;

    private Vector4 velocity;

    private void Awake()
    {
        GameStateMachine.Instance.GetState<MainMenuState>().StartEvent += OnMainMenuStateStarted;

        GameStateMachine.Instance.GetState<StartGameState>().StartEvent += OnStartGameStateStarted;

        TargetController.Instance.TargetUpdatedEvent += OnTargetUpdated;

        currentColor = startColor;

        targetColor = startColor;

        targetMaterial.color = currentColor;
    }

    private void Update()
    {
        if (deadzone.InReach(currentColor, targetColor))
        {
            return;
        }

        currentColor.r = Mathf.SmoothDamp(currentColor.r, targetColor.r, ref velocity.x, colorChangeTime);
        currentColor.g = Mathf.SmoothDamp(currentColor.g, targetColor.g, ref velocity.y, colorChangeTime);
        currentColor.b = Mathf.SmoothDamp(currentColor.b, targetColor.b, ref velocity.z, colorChangeTime);
        currentColor.a = Mathf.SmoothDamp(currentColor.a, targetColor.a, ref velocity.w, colorChangeTime);

        targetMaterial.color = currentColor;
    }

    private void OnMainMenuStateStarted()
    {
        SetTargetColor(startColor);
    }

    private void OnStartGameStateStarted()
    {
        SetTargetColor(startColor);
    }

    private void OnTargetUpdated()
    {
        SetTargetColor(TargetController.Instance.TargetColor);
    }

    private void SmoothChangeFloat(float current, float target, ref float velocity)
    {
        current = Mathf.SmoothDamp(current, target, ref velocity, colorChangeTime);
    }

    private void SetTargetColor(Color color)
    {
        targetColor = color;
    }
}