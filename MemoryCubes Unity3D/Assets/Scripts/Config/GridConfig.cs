using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridConfig : MonoBehaviour
{
	[SerializeField] private AnimationCurve gridSizeCurve;

	public AnimationCurve GridSizeCurve { get { return gridSizeCurve; } }
}