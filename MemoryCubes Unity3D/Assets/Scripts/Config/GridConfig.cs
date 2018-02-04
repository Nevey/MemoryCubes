using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridConfig : MonoBehaviour
{
	[SerializeField] private AnimationCurve gridSizeCurve;

	[SerializeField] private AnimationCurve gridColorCount;

	public AnimationCurve GridSizeCurve { get { return gridSizeCurve; } }

	public AnimationCurve GridColorCount { get { return gridColorCount; } }
}