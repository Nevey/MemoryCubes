using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Base;

public class TileSelector : MonoBehaviourSingleton<TileSelector>
{
	[SerializeField] private Swiper swiper;

	[SerializeField] private FreeTileChecker freeTileChecker;

	[SerializeField] private RoutineUtility routineUtility;

	private List<GameObject> selectedTiles = new List<GameObject>();

    private List<GameObject> previouslySelectedTiles = new List<GameObject>();

	// Use the color magenta to determine unselected color
    private Color previouslySelectedTileColor = Color.magenta;

	private bool canSelect;

	private bool isActive;

	public List<GameObject> SelectedTiles { get { return selectedTiles; } }

    public List<GameObject> PreviouslySelectedTiles { get { return previouslySelectedTiles; } }

	public event Action<List<GameObject>> SelectedTilesUpdatedEvent;

	private void OnEnable()
	{
		canSelect = true;

		GameStateMachine.Instance.GetState<PlayerInputState>().StartEvent += OnPlayerInputStateStarted;

		GameStateMachine.Instance.GetState<PlayerInputState>().FinishedEvent += OnPlayerInputStateFinished;
	}

    private void OnDisable()
	{
		GameStateMachine.Instance.GetState<PlayerInputState>().StartEvent -= OnPlayerInputStateStarted;

		GameStateMachine.Instance.GetState<PlayerInputState>().FinishedEvent -= OnPlayerInputStateFinished;
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
		ClearSelectedTiles();

		EnableInput();
	}

    private void OnPlayerInputStateFinished()
    {
        DisableInput();

		previouslySelectedTileColor = Color.black;
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

            TileColor tileColor = collidedGO.GetComponent<TileColor>();

			if (selector != null && tileColor != null)
			{
				GridCoordinates gridCoordinates = selector.gameObject.GetComponent<GridCoordinates>();

				if (gridCoordinates != null 
					&& freeTileChecker.CanTapTile(gridCoordinates.MyPosition))
				{
					SelectTile(selector, tileColor);
				}
			}
		}
    }

	private void SelectTile(Selector selector, TileColor tileColor)
	{        
		selector.SelectToggledEvent += OnSelectToggled;

        if (previouslySelectedTileColor != tileColor.MyColor
			|| previouslySelectedTileColor == Color.magenta)
        {
            TargetController.Instance.SetNextTarget(tileColor.MyColor);

            previouslySelectedTiles = selectedTiles;

            previouslySelectedTileColor = tileColor.MyColor;
        }

		selector.Toggle(GameModeController.Instance.CurrentGameMode);
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

		previouslySelectedTiles.Clear();
	}
}
