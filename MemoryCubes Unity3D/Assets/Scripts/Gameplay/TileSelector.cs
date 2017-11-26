using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
	[SerializeField] private Swiper swiper;

	[SerializeField] private FreeTileChecker freeTileChecker;

	[SerializeField] private GameModeController gameModeController;

	[SerializeField] private RoutineUtility routineUtility;

	private List<GameObject> selectedTiles = new List<GameObject>();

	private bool canSelect;

	private bool isActive;

	public List<GameObject> SelectedTiles { get { return selectedTiles; } }

	public event Action<List<GameObject>> SelectedTilesUpdatedEvent;

	private void OnEnable()
	{
		canSelect = true;

		PlayerInputState.PlayerInputStateStartedEvent += OnPlayerInputStateStarted;

		PlayerInputState.PlayerInputStateFinishedEvent += OnPlayerInputStateFinished;
	}

    private void OnDisable()
	{
		PlayerInputState.PlayerInputStateStartedEvent -= OnPlayerInputStateStarted;

		PlayerInputState.PlayerInputStateFinishedEvent -= OnPlayerInputStateFinished;
	}
	
	// LateUpdate is called once per frame, after Update
	private void LateUpdate()
	{
		if (!isActive)
		{
			return;
		}
		
        if (Input.GetMouseButtonUp(0))
		{
			// Try tapping a tile
			TapTile();

			// Always set can select back to true after input up
			// This is needed because we don't want a tile to be selected while swiping
			// but we do want the next input up to be selecting a tile when not swiping
			canSelect = true;
		}
	}

	private void OnPlayerInputStateStarted()
	{
		EnableInput();
	}

    private void OnPlayerInputStateFinished()
    {
        DisableInput();

		ClearSelectedTiles();
    }

	private void EnableInput()
	{
		routineUtility.StartWaitOneFrameRoutine(() =>
		{
			isActive = true;

			swiper.SwipeEvent += OnSwipeEvent;
		});
	}

	private void DisableInput()
	{
		isActive = false;

		swiper.SwipeEvent -= OnSwipeEvent;
	}

	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		canSelect = false;
	}

	private void TapTile()
	{
		if (!canSelect)
		{
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		float raycastDistance = 100f;
		
		if (Physics.Raycast(ray, out hit, raycastDistance)) 
		{
			GameObject collidedGO = hit.collider.gameObject;

			Selector selector = collidedGO.GetComponent<Selector>();

			if (selector != null)
			{
				GridCoordinates gridCoordinates = selector.gameObject.GetComponent<GridCoordinates>();

				if (gridCoordinates != null 
					&& freeTileChecker.CanTapTile(gridCoordinates.MyPosition))
				{
					SelectTile(selector);
				}
			}
		}
    }

	private void SelectTile(Selector selector)
	{
		selector.SelectToggledEvent += OnSelectToggled;

		selector.Toggle(gameModeController.CurrentGameMode);
	}

    private void OnSelectToggled(object sender, SelectorArgs e)
    {
		e.selector.SelectToggledEvent -= OnSelectToggled;

		if (e.selectionState == SelectionState.selected)
		{
			if (selectedTiles.Contains(e.selectedObject))
			{
				Debug.LogWarning("Trying to add selected tile to selected tiles list, but the list already contains this tile!");

				return;
			}

			selectedTiles.Add(e.selectedObject);
		}

		if (e.selectionState == SelectionState.notSelected)
		{
			if (!selectedTiles.Contains(e.selectedObject))
			{
				Debug.LogWarning("Trying to remove tile from selected tiles list, but the list does not contain this tile!");

				return;
			}

			selectedTiles.Remove(e.selectedObject);
		}

		DispatchSelectedTilesUpdate();
    }

	private void DispatchSelectedTilesUpdate()
	{
		if (SelectedTilesUpdatedEvent != null)
		{
			SelectedTilesUpdatedEvent(selectedTiles);
		}
	}

    public void ClearSelectedTiles()
	{
		selectedTiles.Clear();
	}
}
