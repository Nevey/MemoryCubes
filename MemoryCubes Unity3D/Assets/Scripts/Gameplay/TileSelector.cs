using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
	[SerializeField] private Swiper swiper;

	private List<GameObject> selectedTiles = new List<GameObject>();

	private bool canSelect = true;

	public List<GameObject> SelectedTiles { get { return selectedTiles; } }
	
	// Update is called once per frame
	private void Update()
	{
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

	public void ClearSelectedTiles()
	{
		selectedTiles.Clear();
	}
}
