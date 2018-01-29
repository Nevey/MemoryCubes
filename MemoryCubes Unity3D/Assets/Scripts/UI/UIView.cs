using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UIView : MonoBehaviour, IUIView
{
	[Header("The UI View ID, all ID's are defined in a class")]
	[SerializeField] private UIViewID uiViewID;

	[Header("Show animation properties")]
	[SerializeField] private AnimationDirection showAnimationDirection;

	[SerializeField] private Ease showAnimationEase = Ease.OutBack;

	[SerializeField] private float showAnimationTime = 1f;

	[Header("Hide animation properties")]
	[SerializeField] private AnimationDirection hideAnimationDirection;

	[SerializeField] private Ease hideAnimationEase = Ease.InBack;

	[SerializeField] private float hideAnimationTime = 1f;

	private enum AnimationDirection
	{
		top,
		bottom,
		left,
		right
	}

	private enum WindowState
	{
		Hidden,
		Showing
	}

	private WindowState windowState = WindowState.Hidden;

	private IOnUIViewInitialize[] onUIViewInitialize;

	private IOnUIViewShow[] onUIViewShow;

	private IOnUIViewHide[] onUIViewHide;

	private Rect uiRect;

	private RectTransform rectTransform;

	private Vector2 startPosition;

	private Vector2 showPosition;

	private Vector2 endPosition;

	public UIViewID UIViewID
	{
		get { return uiViewID; }
	}

	public event Action<UIView> ShowCompleteEvent;

	public event Action<UIView> HideCompleteEvent;

	private void SetupAnimation()
	{
		// The start position used for the "show" animation
		startPosition = GetDirectionPosition(showAnimationDirection);

		// Middle of the screen
		showPosition = Vector2.zero;

		// The end position used for the "hide" animation
		endPosition = GetDirectionPosition(hideAnimationDirection);
	}

	private Vector2 GetDirectionPosition(AnimationDirection direction)
	{
		Vector2 position = Vector2.zero;

		switch (direction)
		{
			case AnimationDirection.top:
				
				position = new Vector2(
					0f,
					uiRect.height
				);

				break;
			
			case AnimationDirection.bottom:

				position = new Vector2(
					0f,
					-uiRect.height
				);

				break;
			
			case AnimationDirection.left:

				position = new Vector2(
					-uiRect.width,
					0f
				);

				break;
			
			case AnimationDirection.right:

				position = new Vector2(
					uiRect.width,
					0f
				);

				break;
		}

		return position;
	}

	/// <summary>
	/// Plays the show animation
	/// </summary>
	private void AnimateIn()
	{
		rectTransform.anchoredPosition = startPosition;

		Tween tween = rectTransform.DOAnchorPos(showPosition, showAnimationTime);

		tween.SetEase(showAnimationEase);

		tween.OnComplete(() =>
		{
			OnShowComplete();

			if (ShowCompleteEvent != null)
			{
				ShowCompleteEvent(this);
			}
		});

		DOTween.Play(tween);
	}

	/// <summary>
	/// Plays the hide animation
	/// </summary>
	private void AnimateOut()
	{
		Tween tween = rectTransform.DOAnchorPos(endPosition, hideAnimationTime);

		tween.SetEase(hideAnimationEase);

		tween.OnComplete(() =>
		{
			OnHideComplete();

			if (HideCompleteEvent != null)
			{
				HideCompleteEvent(this);
			}
		});

		DOTween.Play(tween);
	}

	/// <summary>
	/// Use this method to do custom stuff internally
	/// </summary>
	protected virtual void OnShowComplete()
	{
		// Do custom stuff here...
	}

	/// <summary>
	/// Use this method to do custom stuff internally
	/// </summary>
	protected virtual void OnHideComplete()
	{
		gameObject.SetActive(false);

		for (int i = 0; i < onUIViewHide.Length; i++)
		{
			onUIViewHide[i].OnUIViewHide();
		}
		
		// Do custom stuff here...
	}

	public virtual void Initialize()
	{
		// In case this view was enabled in the editor, disable...
		gameObject.SetActive(false);

		onUIViewInitialize = GetComponentsInChildren<IOnUIViewInitialize>(true);

		onUIViewShow = GetComponentsInChildren<IOnUIViewShow>(true);

		onUIViewHide = GetComponentsInChildren<IOnUIViewHide>(true);

		for (int i = 0; i < onUIViewInitialize.Length; i++)
		{
			onUIViewInitialize[i].OnUIViewInitialize();
		}

		rectTransform = GetComponent<RectTransform>();

		uiRect = rectTransform.rect;

		SetupAnimation();

		name = this.GetType().Name;
	}

	public virtual void Show()
	{
		if (windowState == WindowState.Showing)
		{
			return;
		}

		gameObject.SetActive(true);

		for (int i = 0; i < onUIViewShow.Length; i++)
		{
			onUIViewShow[i].OnUIViewShow();
		}

		windowState = WindowState.Showing;

		AnimateIn();
	}

	public virtual void Hide()
	{
		if (windowState == WindowState.Hidden)
		{
			return;
		}

		windowState = WindowState.Hidden;

		AnimateOut();
	}
}
