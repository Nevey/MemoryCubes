using System;
using DG.Tweening;
using UnityEngine;

public class Resizer : MonoBehaviour 
{
	[SerializeField] private Ease scaleEase;

	[SerializeField] private float scaleTweenTime = 0.5f;

	[SerializeField] private float selectedScale = 0.5f;
	
	private float originScale;
	
	private float targetScale;

	public event Action ResizeAnimationFinishedEvent;

	private void Start() 
	{
		// We want to scale all axes by the same amount, so using scale.x is OK
		originScale = transform.localScale.x;
		
		targetScale = originScale;
	}

	private void DispatchResizeAnimationFinished()
	{
		if (ResizeAnimationFinishedEvent != null)
		{
			ResizeAnimationFinishedEvent();
		}
	}

	private void DoResizeTween(GameMode currentGameMode)
	{
		switch (currentGameMode)
		{
			case GameMode.Combine:

				transform.DOScale(targetScale, scaleTweenTime)
					.OnComplete(DispatchResizeAnimationFinished)
					.SetEase(scaleEase);

				break;
			
			default:

				transform.DOScale(targetScale, scaleTweenTime)
					.SetEase(scaleEase);

				DispatchResizeAnimationFinished();

				break;
		}
	}
	
	public void DoResize(SelectionState selectionState, GameMode currentGameMode)
	{
		if (selectionState == SelectionState.selected)
		{
			targetScale = originScale * selectedScale;
		}
		else
		{
			targetScale = originScale;
		}

		DoResizeTween(currentGameMode);
	}
}
