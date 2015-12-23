using UnityEngine;
using System;

public class SwipeEventArgs : EventArgs
{
	public SwipeDirection direction { get; set; }
}

public enum SwipeDirection
{
	up,
	
	down,
	
	left,
	
	right
}

public class Swiper : MonoBehaviour 
{
	private bool checkForSwipe = false;
	
	private Vector2 inputOrigin = Vector2.zero;
	
	private float swipeDistance = 0f;
	
	private Viewport viewport;
	
	public float maxSwipeDistance = 200f;
	
	public float minSwipeDistance = 10f;
	
	public event EventHandler<SwipeEventArgs> SwipeEvent;
		
	// Use this for initialization
	void Start() 
	{
		viewport = Camera.main.GetComponent<Viewport>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			StartSwipe();
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			EndSwipe();
		}
		
		UpdateSwipe();
	}
	
	private void StartSwipe()
	{
		inputOrigin = Input.mousePosition;
		
		checkForSwipe = true;
	}
	
	private void EndSwipe()
	{
		if (swipeDistance >= minSwipeDistance)
		{
			OnSwipe();
		}
		
		checkForSwipe = false;
	}
	
	private void UpdateSwipe()
	{
		if (!checkForSwipe)
		{
			return;
		}
		
		swipeDistance = Vector2.Distance(inputOrigin, Input.mousePosition);
		
		swipeDistance /= viewport.Scale;
		
		// Debug.Log(swipeDistance);
		
		if (swipeDistance >= maxSwipeDistance)
		{
			OnSwipe();
		}
	}
	
	private void OnSwipe()
	{
		if (!checkForSwipe)
		{
			return;
		}
		
		checkForSwipe = false;
		
		// Create swipe event args
		SwipeEventArgs swipeEventArgs = new SwipeEventArgs();
		
		// Set swipe direction for event args
		swipeEventArgs.direction = CalculateSwipeDirection();
		
		if (SwipeEvent != null)
		{
			SwipeEvent(this, swipeEventArgs);
		}
	}
	
	private SwipeDirection CalculateSwipeDirection()
	{
		// Create fake origin vectors
		Vector2 horizontalOrigin = new Vector2(inputOrigin.x, 0);
		Vector2 verticalOrigin = new Vector2(0, inputOrigin.y);
		
		// Create fake mouse position vectors		
		Vector2 horizontalMousePosition = new Vector2(Input.mousePosition.x, 0);
		Vector2 verticlaMousePosition = new Vector2(0, Input.mousePosition.y);
		
		// Calculate distance of both fake vector axes
		float horizontalSwipeDistance = Vector2.Distance(horizontalOrigin, horizontalMousePosition);
		float verticalSwipeDistance = Vector2.Distance(verticalOrigin, verticlaMousePosition);
		
		// Create swipe direction
		SwipeDirection swipeAxis = new SwipeDirection();
		
		// Horizontal swipe distance is greater than vertical swipe distance
		if (horizontalSwipeDistance > verticalSwipeDistance)
		{
			float swipeDistance = inputOrigin.x - Input.mousePosition.x;
			
			// We swiped left
			if (swipeDistance > 0)
			{
				swipeAxis = SwipeDirection.left;
			}
			// We swiped right
			else
			{
				swipeAxis = SwipeDirection.right;
			}
		}
		// Vertical swipe distance is greater...
		else
		{
			float swipeDistance = inputOrigin.y - Input.mousePosition.y;
			
			// We swiped down
			if (swipeDistance > 0)
			{
				swipeAxis = SwipeDirection.down;
			}
			// We swiped up
			else
			{
				swipeAxis = SwipeDirection.up;
			}
		}
		
		return swipeAxis;
	}
}
