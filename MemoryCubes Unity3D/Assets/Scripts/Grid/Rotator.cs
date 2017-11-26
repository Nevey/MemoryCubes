using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour 
{
	[SerializeField] private Swiper swiper;

	[SerializeField] private Transform rotationHelper;

	[SerializeField] private float rotationTime = 0.5f;
	
	[SerializeField] private Vector2 rotationAmount;

	private void OnEnable()
	{
		swiper.SwipeEvent += OnSwipeEvent;
	}

	private void OnDisable()
	{
		swiper.SwipeEvent -= OnSwipeEvent;
	}
	
	private void OnSwipeEvent(object sender, SwipeEventArgs e)
	{
		Turn(e.direction);

		DoTurnTween();
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

	/// <summary>
	/// Apply rotation tween to transform
	/// </summary>
	private void DoTurnTween()
	{
		transform.DORotateQuaternion(rotationHelper.transform.rotation, rotationTime).SetEase(Ease.OutBack);
	}
}
