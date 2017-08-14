using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
	[SerializeField] private Swiper swiper;

	private List<GameObject> selectedTiles = new List<GameObject>();

	private bool isEnabled = false;

	private bool canSelect = true;

	public List<GameObject> SelectedTiles { get { return selectedTiles; } }
	
	private void OnEnable()
	{
        PlayerSelectingCubesState.SelectingCubesStateStartedEvent += OnSelectingCubesStateStarted;

        PlayerCollectingCubesState.CollectingCubesStateStartedEvent += OnCollectingCubesStateStarted;
	}

	private void OnDisable()
	{
		PlayerSelectingCubesState.SelectingCubesStateStartedEvent -= OnSelectingCubesStateStarted;

        PlayerCollectingCubesState.CollectingCubesStateStartedEvent -= OnCollectingCubesStateStarted;
	}
	
	// Update is called once per frame
	private void Update()
	{
		if (!isEnabled)
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

	private void OnSelectingCubesStateStarted()
    {
        EnableSelecting();
    }

    private void OnCollectingCubesStateStarted()
    {
        DisableSelecting();
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

	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		canSelect = false;
	}

	private void TapTile()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		float raycastDistance = 100f;
		
		if (Physics.Raycast(ray, out hit, raycastDistance)) 
		{
			Selector selector = hit.collider.gameObject.GetComponent<Selector>();

			if (selector != null)
			{
				if (canSelect)
				{
					selector.Select();

					selectedTiles.Add(hit.collider.gameObject);
				}
			}
		}        
    }
}
