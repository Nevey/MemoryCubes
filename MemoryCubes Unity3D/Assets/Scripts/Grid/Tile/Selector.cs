using UnityEngine;
using System;

public enum SelectionState
{
	notSelected,
	selected
}

public class SelectorArgs : EventArgs
{
	public Selector selector { get; set; }
	
	public GameObject selectedObject { get; set; }
	
	public SelectionState selectionState { get; set; }
}

[RequireComponent(typeof(Resizer))]
public class Selector : MonoBehaviour 
{
	private Resizer resizer;

	private SelectionState selectionState = new SelectionState();

    public SelectionState CurrentSelection
    {
        get { return selectionState; }
    }
    
	public event EventHandler<SelectorArgs> SelectToggledEvent;

	private void Start()
	{
		resizer = GetComponent<Resizer>();

		resizer.ResizeAnimationFinishedEvent += OnResizeAnimationFinished;
	}

	private void OnDestroy()
	{
		resizer.ResizeAnimationFinishedEvent -= OnResizeAnimationFinished;
	}

    private void OnResizeAnimationFinished(Resizer resizer)
    {
		// Create selector event args
		SelectorArgs selectorArgs = new SelectorArgs();

		selectorArgs.selector = this;
		
		selectorArgs.selectedObject = gameObject;
		
		selectorArgs.selectionState = selectionState;

        // Send select event
		if (SelectToggledEvent != null)
		{
			SelectToggledEvent(this, selectorArgs);
		}
    }

    public void Toggle(GameMode currentGameMode)
	{
        // Set selection state
        if (selectionState == SelectionState.notSelected)
		{
			selectionState = SelectionState.selected;
		}
		else
		{
			selectionState = SelectionState.notSelected;
		}

		resizer.DoSelectionResize(selectionState, currentGameMode);
	}
}
