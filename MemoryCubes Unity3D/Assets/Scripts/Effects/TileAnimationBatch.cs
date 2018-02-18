using System;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationBatch
{
    private List<GameObject> tileList = new List<GameObject>();

    private Action callback;

    private int totalAnimationCount;

    public event Action<TileAnimationBatch> AnimationBatchFinishedEvent;

    public TileAnimationBatch(List<GameObject> tileList, Action callback = null)
    {
        this.tileList = tileList;

        this.callback = callback;

        totalAnimationCount = tileList.Count;
    }

    private void PlayDestroyAnimation(TileAnimator tileAnimator)
    {
        // TODO: Move spawning of these particles to Destroyer
        ParticlesSpawner.Instance.Spawn(
            tileAnimator.transform,
            tileAnimator.GetComponent<TileColor>().MyColor
        );

		Destroyer destroyer = tileAnimator.GetComponent<Destroyer>();

		destroyer.DestroyCube();
    }

    private void OnCollectAnimationFinished(TileAnimator tileAnimator)
    {
        PlayDestroyAnimation(tileAnimator);

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
        for (int i = 0; i < tileList.Count; i++)
        {
            TileAnimator tileAnimator = tileList[i].GetComponent<TileAnimator>();

            // NOTE: Not playing a tween when collecting during gameplay
            PlayDestroyAnimation(tileAnimator);
        }

        // Have to loop twice as we don't want to mess up the list
        for (int i = 0; i < tileList.Count; i++)
        {
            TileAnimator tileAnimator = tileList[i].GetComponent<TileAnimator>();

            CheckForAnimationBatchFinished(tileAnimator);
        }
    }

    public void PlayCollectLastStandingTilesAnimation(float resizeDelay, float collectDelay)
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            TileAnimator tileAnimator = tileList[i].GetComponent<TileAnimator>();

            float delay = resizeDelay * i;

            tileAnimator.DoSelectedTween(delay, () =>
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