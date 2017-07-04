using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
	private Swiper swiper;

	private bool isEnabled = false;

	private bool canSelect = true;
	
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
				}
			}
		}        
    }
}
