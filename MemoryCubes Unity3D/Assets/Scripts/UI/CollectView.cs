using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectView : MonoBehaviour
{
	[SerializeField] private Button collectButton;

	[SerializeField] private CollectController collectController;

	private void OnEnable()
	{
		collectButton.onClick.AddListener(collectController.CollectSelectedTiles);

		collectButton.enabled = true;
	}

	private void OnDisable()
	{
		collectButton.onClick.RemoveListener(collectController.CollectSelectedTiles);

		collectButton.enabled = false;
	}
}
