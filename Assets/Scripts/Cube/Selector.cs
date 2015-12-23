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
	private Swiper swiper;
	
	private SelectionState selectionState = new SelectionState();
	
	private bool canSelect = true;
	
	public event EventHandler<SelectorArgs> SelectEvent;
	
	// Use this for initialization
	void Start() 
	{
		swiper = GameObject.Find("GridParent").GetComponent<Swiper>();
		
		swiper.SwipeEvent += OnSwipeEvent;
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (Input.GetMouseButtonUp(0))
		{
			TapMe();
		}
	}
	
	private void TapMe()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		float raycastDistance = 100f;
		
		if (Physics.Raycast(ray, out hit, raycastDistance)) 
		{			
			if (hit.collider.gameObject == gameObject)
			{
				Select();
			}
		}
	}
	
	private void Select()
	{
		// If swiping, don't select anything
		if (!canSelect)
		{
			canSelect = true;
			
			return;
		}
		
		// Set selection state
		if (selectionState == SelectionState.notSelected)
		{
			selectionState = SelectionState.selected;
		}
		else
		{
			selectionState = SelectionState.notSelected;
		}
		
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
	
	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		canSelect = false;
	}
}
