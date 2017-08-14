using UnityEngine;
using System;

public class NextTargetEventArgs : EventArgs
{
    public Color targetColor { get; set; }
    public bool isFirstTarget { get; set; }
}

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private ColorConfig colorConfig;

    private bool isFirstTarget = true;

    public event EventHandler<NextTargetEventArgs> NextTargetEvent;    

	// Use this for pre-initialization
	private void OnEnable() 
    {
        isFirstTarget = true;

        SelectColorTargetState.SelectColorTargetStateStartedEvent += OnSelectColorTargetStateStarted;
	}

    private void OnDisable()
    {
        SelectColorTargetState.SelectColorTargetStateStartedEvent -= OnSelectColorTargetStateStarted;
    }

    private void OnSelectColorTargetStateStarted()
    {
        SetNextTarget(isFirstTarget);

        isFirstTarget = false;
    }
    
    private void SetNextTarget(bool isFirstTarget = false)
    {
        NextTargetEventArgs args = new NextTargetEventArgs();
        
        args.targetColor = colorConfig.GetRandomColor();

        args.isFirstTarget = isFirstTarget;
        
        Debug.Log("New target set: " + args.targetColor);
        
        if (NextTargetEvent != null)
        {
            NextTargetEvent(this, args);
        }
    }
}
