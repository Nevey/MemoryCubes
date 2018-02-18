using System;
using DG.Tweening;
using UnityEngine;

public class TileAnimator : MonoBehaviour 
{
	[Header("Scale amount when cube is built, before scaling it to 1")]
	[SerializeField] private Ease startScaleEase;

	[SerializeField] private float startScaleTime = 0.5f;

	[Header("Select Tween Values")]
	[SerializeField] private Ease selectScaleEase;

	[SerializeField] private float selectScaleTime = 0.5f;

	[SerializeField] private float selectScaleValue = 0.5f;

	[Header("Grid Animation Tween Values")]
	[SerializeField] private float gridScaleValue = 1.1f;

	[SerializeField] private float gridScaleTime = 0.2f;

	[Header("Collect Tween Values")]
	[SerializeField] private float destroyScaleValue = 1.15f;

	[SerializeField] private float destroyScaleTime = 0.4f;

	private Selector selector;

	private float originScale;
	
	private float targetScale;

	private Sequence gridTweenSequence;

	public event Action<TileAnimator> ResizeAnimationFinishedEvent;

	private void Awake()
	{
		selector = GetComponent<Selector>();
	}

	private void Update()
	{
		// TODO: Remove this debug code
		if (Input.GetKeyDown(KeyCode.W))
		{
			DoGridScaleUpTween(0f);
		}
	}

	private float GetCurrentScale(SelectionState selectionState)
	{
		if (selectionState == SelectionState.selected)
		{
			return originScale * selectScaleValue;
		}
		else
		{
			return originScale;
		}
	}

	private void KillSequence()
	{
		if (gridTweenSequence != null && gridTweenSequence.IsPlaying())
		{
			gridTweenSequence.Kill();
		}
	}

	public void SetOriginScale(float scale)
	{
		originScale = scale;
	}

	public void DoStartupResize(float delay = 0f, TweenCallback callback = null)
	{
		targetScale = GetCurrentScale(SelectionState.notSelected);

		transform.DOScale(targetScale, startScaleTime)
			.SetEase(startScaleEase)
			.SetDelay(delay)
			.OnComplete(callback);
	}

	public void DoSelectedTween(float delay = 0f, TweenCallback callback = null)
	{
		KillSequence();

		float scale = originScale * selectScaleValue;

		transform.DOScale(scale, selectScaleTime)
			.SetEase(selectScaleEase)
			.SetDelay(delay)
			.OnComplete(callback);
	}

	public void DoUnselectedTween(float delay = 0f, TweenCallback callback = null)
	{
		KillSequence();

		transform.DOScale(originScale, selectScaleTime)
			.SetEase(selectScaleEase)
			.SetDelay(delay)
			.OnComplete(callback);
	}

	public void DoCollectTween(float delay, TweenCallback callback)
	{
		KillSequence();

		float scale = originScale * destroyScaleValue;

		transform.DOScale(scale, destroyScaleTime)
			.SetEase(Ease.InOutBack)
			.SetDelay(delay)
			.OnComplete(callback);
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
}
