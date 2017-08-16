using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
	[SerializeField] private Swiper swiper;

	[SerializeField] private FreeTileChecker freeTileChecker;

	private List<GameObject> selectedTiles = new List<GameObject>();

	private bool canSelect;

	private bool isActive;

	public List<GameObject> SelectedTiles { get { return selectedTiles; } }

	private void OnEnable()
	{
		canSelect = true;

		PlayerInputState.PlayerInputStateStartedEvent += OnPlayerInputStateStarted;

		GameOverState.GameOverStateStartedEvent += OnGameOverStateStarted;
	}

	private void OnDisable()
	{
		PlayerInputState.PlayerInputStateStartedEvent -= OnPlayerInputStateStarted;

		GameOverState.GameOverStateStartedEvent -= OnGameOverStateStarted;
	}
	
	// Update is called once per frame
	private void Update()
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
			// TODO: Only works when not releasing input before swiping was done
			canSelect = true;
		}
	}

	private void OnPlayerInputStateStarted()
	{
		EnableInput();
	}

	private void OnGameOverStateStarted()
	{
		ClearSelectedTiles();
		
		DisableInput();
	}

	private void EnableInput()
	{
		isActive = true;

		swiper.SwipeEvent += OnSwipeEvent;
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
			Selector selector = hit.collider.gameObject.GetComponent<Selector>();

			GridCoordinates gridCoordinates = hit.collider.gameObject.GetComponent<GridCoordinates>();

			if (selector != null)
			{
				if (gridCoordinates != null 
					&& freeTileChecker.CanTapTile(gridCoordinates.MyPosition))
				{
					selector.Select();

					selectedTiles.Add(hit.collider.gameObject);
				}
			}
		}        
    }

	public void ClearSelectedTiles()
	{
		selectedTiles.Clear();
	}
}
