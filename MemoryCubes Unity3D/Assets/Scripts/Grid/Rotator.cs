using UnityEngine;

public class Rotator : MonoBehaviour 
{
	[SerializeField] private Transform rotationHelper;
	
	[SerializeField] private float smoothStrength = 0.1f;
	
	[SerializeField] private Vector2 rotationAmount;

	[SerializeField] private Swiper swiper;

	private void OnEnable()
	{
		swiper.SwipeEvent += OnSwipeEvent;
	}

	private void OnDisable()
	{
		swiper.SwipeEvent -= OnSwipeEvent;
	}
	
	// Update is called once per frame
	private void Update()
	{
		// We lerp quaternion values to the desired rotation, this way we work around euler rotation issues
		transform.rotation = Quaternion.Lerp(transform.rotation, rotationHelper.rotation, smoothStrength);
	}
	
	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		Turn(e.direction);
	}
	
	/// <summary>
	/// Turning the rotation helper to the desired rotation
	/// </summary>
	/// <param name="swipeDirection"></param>
	private void Turn(SwipeDirection swipeDirection)
	{
		switch (swipeDirection)
		{
			case SwipeDirection.up:

				rotationHelper.Rotate(new Vector3(rotationAmount.y, 0, 0), Space.World);

			break;
			
			case SwipeDirection.down:

				rotationHelper.Rotate(new Vector3(-rotationAmount.y, 0, 0), Space.World);

			break;
			
			case SwipeDirection.left:

				rotationHelper.Rotate(new Vector3(0, rotationAmount.x, 0), Space.World);

			break;
			
			case SwipeDirection.right:

				rotationHelper.Rotate(new Vector3(0, -rotationAmount.x, 0), Space.World);
				
			break;
		}
	}
}
