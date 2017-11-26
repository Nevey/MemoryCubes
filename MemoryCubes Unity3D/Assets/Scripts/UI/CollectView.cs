using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectView : MonoBehaviour, IOnUIViewInitialize, IOnUIViewShow, IOnUIViewHide
{
	[SerializeField] private Button collectButton;

	[SerializeField] private CollectController collectController;

	[SerializeField] private GameModeCorresponder gameModeCorresponder;

	[SerializeField] private GameModeController gameModeController;

    public void OnUIViewInitialize()
    {
        gameObject.SetActive(false);
    }

    public void OnUIViewShow()
    {
        collectButton.onClick.AddListener(collectController.CollectSelectedTiles);

		collectButton.enabled = true;

		bool isActive = gameModeCorresponder.CorrespindingGameMode == gameModeController.CurrentGameMode;

		gameObject.SetActive(isActive);
    }

    public void OnUIViewHide()
    {
        collectButton.onClick.RemoveListener(collectController.CollectSelectedTiles);

		collectButton.enabled = false;
    }
}
