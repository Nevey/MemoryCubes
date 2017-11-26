using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour, IUIView
{
	[SerializeField] private UIViewType uiViewType;

	private IOnUIViewInitialize[] onUIViewInitialize;

	private IOnUIViewShow[] onUIViewShow;

	private IOnUIViewHide[] onUIViewHide;

	public UIViewType UIViewType
	{
		get { return uiViewType; }
	}

	public void Initialize()
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
	}

	public void Show()
	{
		gameObject.SetActive(true);

		for (int i = 0; i < onUIViewShow.Length; i++)
		{
			onUIViewShow[i].OnUIViewShow();
		}
	}

	public void Hide()
	{
		gameObject.SetActive(false);

		for (int i = 0; i < onUIViewHide.Length; i++)
		{
			onUIViewHide[i].OnUIViewHide();
		}
	}
}
