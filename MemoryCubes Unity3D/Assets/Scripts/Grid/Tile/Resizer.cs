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

	[Header("Grid Scaling Values")]
	[SerializeField] private float gridScaleValue = 1.1f;

	[SerializeField] private float gridScaleTime = 0.2f;

	[Header("Destroy Tile Values")]
	[SerializeField] private float destroyScaleValue = 1.15f;

	[SerializeField] private float destroyScaleTime = 0.4f;

	private Selector selector;

	private float originScale;
	
	private float targetScale;

	private Sequence gridTweenSequence;

	public event Action<Resizer> ResizeAnimationFinishedEvent;

	private void Awake()
	{
		selector = GetComponent<Selector>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			DoGridScaleUpTween(0f);
		}
	}

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

	private float GetCurrentScale(SelectionState selectionState)
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
		if (gridTweenSequence != null && gridTweenSequence.IsPlaying())
		{
			gridTweenSequence.Kill();
		}

		targetScale = GetCurrentScale(selectionState);

		DoResizeTween(currentGameMode);
	}

	public void DoSelectionResize(float delay, SelectionState selectionState)
	{
		transform.DOScale(GetCurrentScale(selectionState), scaleTweenTime)
			.SetEase(scaleEase);
	}

	public void DoStartupResize(float delay)
	{
		targetScale = GetCurrentScale(SelectionState.notSelected);

		transform.DOScale(targetScale, startScaleTime)
			.SetEase(startScaleEase)
			.SetDelay(delay)
			.OnComplete(DispatchResizeAnimationFinished);
	}

	public void DoGridScaleUpTween(float delay)
	{
		// TODO: Look into re-using the same sequence
		gridTweenSequence = DOTween.Sequence();

		float scale = GetCurrentScale(selector.SeletionState);

		Tweener tweenUp = transform.DOScale(scale * gridScaleValue, gridScaleTime);
		tweenUp.SetEase(Ease.InOutQuad);

		Tweener tweenDown = transform.DOScale(scale, gridScaleTime);
		tweenDown.SetEase(Ease.InOutQuad);

		gridTweenSequence.Append(tweenUp);

		gridTweenSequence.Append(tweenDown);

		gridTweenSequence.PrependInterval(delay);
	}

	public void DoCollectTween(float delay, Action callback)
	{
		if (gridTweenSequence != null && gridTweenSequence.IsPlaying())
		{
			gridTweenSequence.Kill();
		}

		transform.DOScale(originScale * destroyScaleValue, destroyScaleTime)
			.SetEase(Ease.InOutBack)
			.SetDelay(delay)
			.OnComplete(() =>
			{
				if (callback != null)
				{
					callback();
				}
			});
	}
}
