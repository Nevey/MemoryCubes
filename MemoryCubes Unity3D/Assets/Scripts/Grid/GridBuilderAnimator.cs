using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridBuilder))]
public class GridBuilderAnimator : MonoBehaviour
{
    [Header("After this amount of animations, do lock the tween delays")]
    [SerializeField] private int maxAnimations = 8;

    [SerializeField] private float startScaleDelay = 0.1f;

    private GridBuilder gridBuilder;

    private int currentAnimationCount = 0;

    private Action callback;

    private void OnResizeAnimationFinished(Resizer resizer)
    {
        resizer.ResizeAnimationFinishedEvent -= OnResizeAnimationFinished;

        currentAnimationCount--;

        if (currentAnimationCount == 0)
        {
            callback();
        }
    }

    public void AnimateTiles(List<GameObject> flattenedGridList, Action callback)
	{
        currentAnimationCount = flattenedGridList.Count - 1;

        this.callback = callback;

        List<GameObject> tempList = new List<GameObject>(flattenedGridList);

        tempList.Shuffle();

        int index = 0;

		for (int i = 0; i < tempList.Count; i++)
		{
			GameObject tile = tempList[i];

            tile.transform.localScale = Vector3.zero;

			Resizer resizer = tile.GetComponent<Resizer>();

			resizer.ResizeAnimationFinishedEvent += OnResizeAnimationFinished;

            float delay = startScaleDelay * index;

			resizer.DoStartupResize(delay);

            if (index < maxAnimations)
            {
                index++;
            }
		}
	}
}