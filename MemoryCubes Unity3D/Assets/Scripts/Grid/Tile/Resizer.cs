using System;
using UnityEngine;

public class Resizer : MonoBehaviour 
{
	[SerializeField] private float selectedScale = 0.5f;
	
	[SerializeField] private float scalingSmoothTime = 1f;
	
	[SerializeField] private float maxScalingSpeed = 1f;

	private bool isActive;
	
	private float originScale;
	
	private float targetScale;
	
	private Vector3 scalingVelocity = Vector3.zero;
	
	private Deadzone deadzone = new Deadzone();

	public event Action ResizeAnimationFinishedEvent;

	private void Start() 
	{
		// We want to scale all axes by the same amount, so using scale.x is OK
		originScale = transform.localScale.x;
		
		targetScale = originScale;
	}

	private void Update() 
	{
		if (!isActive)
		{
			return;
		}

		Vector3 scale = new Vector3(targetScale, targetScale, targetScale);
		
		transform.localScale = Vector3.SmoothDamp(
			transform.localScale, 
			scale, 
			ref scalingVelocity, 
			scalingSmoothTime, 
			maxScalingSpeed
		);

		if (!deadzone.OutOfReach(transform.localScale, scale))
		{
			DispatchResizeAnimationFinished();

			isActive = false;
		}
	}

	private void DispatchResizeAnimationFinished()
	{
		if (ResizeAnimationFinishedEvent != null)
		{
			ResizeAnimationFinishedEvent();
		}
	}
	
	public void DoResize(SelectionState selectionState)
	{
		if (selectionState == SelectionState.selected)
		{
			targetScale = originScale * selectedScale;
		}
		else
		{
			targetScale = originScale;
		}

		isActive = true;
	}
}
