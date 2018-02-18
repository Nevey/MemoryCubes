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

    /// <summary>
    /// Spawn particles and destroy tile gameobject
    /// </summary>
    /// <param name="tileAnimator"></param>
    private void OnCollectAnimationFinished(TileAnimator tileAnimator)
    {
        // TODO: Move spawning of these particles to Destroyer
        ParticlesSpawner.Instance.Spawn(
            tileAnimator.transform,
            tileAnimator.GetComponent<TileColor>().MyColor
        );

		Destroyer destroyer = tileAnimator.GetComponent<Destroyer>();

		destroyer.DestroyCube();

        CheckForAnimationBatchFinished(tileAnimator);
    }

    /// <summary>
    /// Check if this animation batch has finished
    /// </summary>
    /// <param name="tileAnimator"></param>
    private void CheckForAnimationBatchFinished(TileAnimator tileAnimator)
    {
        tileList.Remove(tileAnimator.gameObject);

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
            TileAnimator tileAnimator = tileList[i].GetComponent<TileAnimator>();

            float delay = batchDelay + (delayPerTile * i);

            tileAnimator.DoCollectTween(delay, () =>
            {
                OnCollectAnimationFinished(tileAnimator);
            });
        }
    }

    public void PlayCollectLastStandingTilesAnimation(float delayPerTile, float resizeDelay, float batchDelay = 0f)
    {
        this.delayPerTile = delayPerTile;

        for (int i = 0; i < tileList.Count; i++)
        {
            TileAnimator tileAnimator = tileList[i].GetComponent<TileAnimator>();

            resizeDelay *= i;

            float collectDelay = batchDelay + (delayPerTile * i);

            tileAnimator.DoSelectedTween(resizeDelay, () =>
            {
                tileAnimator.DoCollectTween(collectDelay, () =>
                {
                    OnCollectAnimationFinished(tileAnimator);
                });
            });

        }
    }

    public void PlayBuildAnimation(float delayPerTile)
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            GameObject tile = tileList[i];

            tile.transform.localScale = Vector3.zero;
 
            TileAnimator tileAnimator = tile.GetComponent<TileAnimator>();

            float delay = delayPerTile * i;

            tileAnimator.DoStartupResize(delay, () =>
            {
                CheckForAnimationBatchFinished(tileAnimator);
            });
        }
    }
}