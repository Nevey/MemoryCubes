using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectView : MonoBehaviour, IOnUIViewInitialize, IOnUIViewShow, IOnUIViewHide
{
	[SerializeField] private Button collectButton;

	[SerializeField] private GameModeCorresponder gameModeCorresponder;

    public void OnUIViewInitialize()
    {
        gameObject.SetActive(false);
    }

    public void OnUIViewShow()
    {
        collectButton.onClick.AddListener(CollectController.Instance.CollectSelectedTiles);

		collectButton.enabled = true;

		bool isActive = gameModeCorresponder.CorrespindingGameMode == GameModeController.Instance.CurrentGameMode;

		gameObject.SetActive(isActive);
    }

    public void OnUIViewHide()
    {
        collectButton.onClick.RemoveListener(CollectController.Instance.CollectSelectedTiles);

		collectButton.enabled = false;
    }
}
