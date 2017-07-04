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

    private bool isEnabled = false;
	private bool canSelect = true;
	
    public SelectionState CurrentSelection
    {
        get { return selectionState; }
    }
    
	public event EventHandler<SelectorArgs> SelectEvent;
	
	// Use this for initialization
	void Awake() 
	{
		swiper = GameObject.Find("GridParent").GetComponent<Swiper>();

        PlayerSelectingCubesState.SelectingCubesStateStartedEvent += OnSelectingCubesStateStarted;

        PlayerCollectingCubesState.CollectingCubesStateStartedEvent += OnCollectingCubesStateStarted;
	}

    // Update is called once per frame
    void Update() 
	{
        if (!isEnabled)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
		{
			TapMe();
		}
	}

    private void OnSelectingCubesStateStarted()
    {
        EnableSelecting();

		Debug.Log("ENABLE SELECTING");
    }

    private void OnCollectingCubesStateStarted()
    {
        DisableSelecting();

		Debug.Log("SELECTING DISABLED!!!!");
    }

    private void EnableSelecting()
    {
        isEnabled = true;

        swiper.SwipeEvent += OnSwipeEvent;
    }

    private void DisableSelecting()
    {
        isEnabled = false;

        swiper.SwipeEvent -= OnSwipeEvent;
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

        canSelect = true;
    }
	
	private void Select()
	{
        // If cannot select, return
        if (!canSelect)
        {
            return;
        }

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
	
	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		canSelect = false;
	}
}
