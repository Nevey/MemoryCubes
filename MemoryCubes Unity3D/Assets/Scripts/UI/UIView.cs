using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour, IUIView
{
	[SerializeField] private UIViewType uiViewType;

	public UIViewType UIViewType
	{
		get { return uiViewType; }
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
