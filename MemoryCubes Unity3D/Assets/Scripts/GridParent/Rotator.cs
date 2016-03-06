using UnityEngine;

public class Rotator : MonoBehaviour 
{
	public Transform rotationHelper;
	
	public float smoothStrength = 0.1f;
	
	public float rotationAngle = 90f;
	
	// Use this for initialization
	void Start()
	{
		Swiper swiper = this.GetComponent<Swiper>();
		
		swiper.SwipeEvent += OnSwipeEvent;
	}
	
	// Update is called once per frame
	void Update()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, rotationHelper.rotation, smoothStrength);
	}
	
	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		Turn(e.direction);
	}
	
	private void Turn(SwipeDirection swipeDirection)
	{
		switch (swipeDirection)
		{
			case SwipeDirection.up:
				rotationHelper.Rotate(new Vector3(rotationAngle, 0, 0), Space.World);
			break;
			
			case SwipeDirection.down:
				rotationHelper.Rotate(new Vector3(-rotationAngle, 0, 0), Space.World);
			break;
			
			case SwipeDirection.left:
				rotationHelper.Rotate(new Vector3(0, rotationAngle, 0), Space.World);
			break;
			
			case SwipeDirection.right:
				rotationHelper.Rotate(new Vector3(0, -rotationAngle, 0), Space.World);
			break;
		}
	}
}
