using UnityEngine;
using System;

public class SelectorArgs : EventArgs
{
	public GameObject selectedObject { get; set; }
	
	public SelectionState selectionState { get; set; }
}

public enum SelectionState
{
	notSelected,
	selected
}

public class Selector : MonoBehaviour 
{
	private SelectionState selectionState = new SelectionState();

    public SelectionState CurrentSelection
    {
        get { return selectionState; }
    }
    
	public event EventHandler<SelectorArgs> SelectEvent;
	
	public void Select()
	{
        // Set selection state
        ToggleSelect();
		
		// Create selector event args
		SelectorArgs selectorArgs = new SelectorArgs();
		
		selectorArgs.selectedObject = gameObject;
		
		selectorArgs.selectionState = selectionState;
		
		// Send select event
		if (SelectEvent != null)
		{
			SelectEvent(this, selectorArgs);
		}
	}
    
    private void ToggleSelect()
    {
		if (selectionState == SelectionState.notSelected)
		{
			selectionState = SelectionState.selected;
		}
		else
		{
			selectionState = SelectionState.notSelected;
		}
    }
}
