using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: Rename... to GameModeButton
public class ButtonGameModeCorresponder : GameModeCorresponder
{
	[SerializeField] private Button button;

	public Button Button { get { return button; } }
}
