using System;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationBatch
{
    private List<GameObject> tileList = new List<GameObject>();

    private Action callback;

    private int totalAnimationCount;

    private float delayPerTile = 0f;

    /// <summary>
    /// Will only return a value bigger than "0" if animation batch is playing
    /// </summary>
    /// <returns>float</returns>
    public float MyDuration
    {
        get
        {
            return totalAnimationCount * delayPerTile;
        }
    }

    public event Action<TileAnimationBatch> AnimationBatchFinishedEvent;

    public TileAnimationBatch(List<GameObject> tileList, Action callback = null)
    {
        this.tileList = tileList;

        this.callback = callback;

        totalAnimationCount = tileList.Count;
    }

    private void OnCollectAnimationFinished(Resizer resizer)
    {
        tileList.Remove(resizer.gameObject);

        // TODO: Move spawning of these particles to Destroyer
        ParticlesSpawner.Instance.Spawn(
            resizer.transform,
            resizer.GetComponent<TileColor>().MyColor
        );

		Destroyer destroyer = resizer.GetComponent<Destroyer>();

		destroyer.DestroyCube();

        CheckForAnimationBatchFinished();
    }

    private void CheckForAnimationBatchFinished()
    {
        if (tileList.Count == 0)
        {
            if (AnimationBatchFinishedEvent != null)
            {
                AnimationBatchFinishedEvent(this);
            }

            if (callback != null)
            {
                callback();
            }

        }
    }

    public void PlayCollectAnimation(float delayPerTile, float batchDelay = 0f)
    {
        this.delayPerTile = delayPerTile;

        for (int i = 0; i < tileList.Count; i++)
        {
            // TODO: Move resizer/animator logic to destroyer?

            Resizer resizer = tileList[i].GetComponent<Resizer>();

            float delay = batchDelay + (delayPerTile * i);

            resizer.DoCollectTween(delay, () =>
            {
                OnCollectAnimationFinished(resizer);
            });
        }
    }

    public void PlayCollectLastStandingTilesAnimation(float delayPerTile, float batchDelay = 0f)
    {
        this.delayPerTile = delayPerTile;

        for (int i = 0; i < tileList.Count; i++)
        {
            // TODO: Move resizer/animator logic to destroyer?

            Resizer resizer = tileList[i].GetComponent<Resizer>();

            float resizeDelay = 0.2f * i;

            float collectDelay = batchDelay + (delayPerTile * i);

            resizer.DoSelectionResize(resizeDelay, SelectionState.selected);

            resizer.DoCollectTween(collectDelay, () =>
            {
                OnCollectAnimationFinished(resizer);
            });
        }
    }
}