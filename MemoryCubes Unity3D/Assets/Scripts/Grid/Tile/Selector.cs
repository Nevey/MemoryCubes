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

[RequireComponent(typeof(TileAnimator))]
public class Selector : MonoBehaviour 
{
	private TileAnimator tileAnimator;

	private SelectionState selectionState = new SelectionState();

    public SelectionState SeletionState
    {
        get { return selectionState; }
    }
    
	public event EventHandler<SelectorArgs> SelectToggledEvent;

	private void Start()
	{
		tileAnimator = GetComponent<TileAnimator>();
	}

	private void OnSelectTweenFinished()
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
			Select(currentGameMode);
		}
		else
        {
            UnSelect(currentGameMode);
        }
    }

    public void Select(GameMode currentGameMode)
	{
		// TODO: Figure out game mode specifics...

		selectionState = SelectionState.selected;
		
		tileAnimator.DoSelectedTween(0f, OnSelectTweenFinished);
	}

    public void UnSelect(GameMode currentGameMode)
    {
		// TODO: Figure out game mode specifics...

        selectionState = SelectionState.notSelected;

        tileAnimator.DoUnselectedTween(0f, OnSelectTweenFinished);
    }
}
