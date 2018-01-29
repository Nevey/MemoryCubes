using System;
using DG.Tweening;
using UnityEngine;

public class Resizer : MonoBehaviour 
{
	[Header("Scale amount when cube is built, before scaling it to 1")]
	[SerializeField] private Ease startScaleEase;

	[SerializeField] private float startScaleTime = 0.5f;

	[Header("Gameplay tween values")]
	[SerializeField] private Ease scaleEase;

	[SerializeField] private float scaleTweenTime = 0.5f;

	[SerializeField] private float selectedScale = 0.5f;

	private float originScale;
	
	private float targetScale;

	public event Action<Resizer> ResizeAnimationFinishedEvent;

	private void DispatchResizeAnimationFinished()
	{
		if (ResizeAnimationFinishedEvent != null)
		{
			ResizeAnimationFinishedEvent(this);
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

	private float GetTargetScale(SelectionState selectionState)
	{
		if (selectionState == SelectionState.selected)
		{
			return originScale * selectedScale;
		}
		else
		{
			return originScale;
		}
	}

	public void SetOriginScale(float scale)
	{
		originScale = scale;
	}
	
	public void DoSelectionResize(SelectionState selectionState, GameMode currentGameMode)
	{
		targetScale = GetTargetScale(selectionState);

		DoResizeTween(currentGameMode);
	}

	public void DoStartupResize(float delay)
	{
		targetScale = GetTargetScale(SelectionState.notSelected);

		transform.DOScale(targetScale, startScaleTime)
			.SetEase(startScaleEase)
			.SetDelay(delay)
			.OnComplete(DispatchResizeAnimationFinished);
	}
}
