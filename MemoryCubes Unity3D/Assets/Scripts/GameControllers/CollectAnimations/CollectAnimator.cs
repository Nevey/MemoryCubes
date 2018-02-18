using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: After making it more generic, rename to GridAnimator
public class CollectAnimator : MonoBehaviour
{    
    [SerializeField] private float delayPerTile = 0.15f;

    private List<TileAnimationBatch> animationBatches = new List<TileAnimationBatch>();

    /// <summary>
    /// This value is based on currently playing batches.
    /// If there's a batch that's not playing, it will not be counted.
    /// </summary>
    /// <returns>float</returns>
    private float GetCurrentBatchDelay()
    {
        float batchDelay = 0f;

        for (int i = 0; i < animationBatches.Count; i++)
        {
            batchDelay += animationBatches[i].MyDuration;
        }

        return batchDelay;
    }

    // TODO: Create an enum to determine which animation we're going to play - making it more generic
    // Use this enum for the Resizer too? And rename resizer to TileAnimator
    public void PlayCollectAnimation(List<GameObject> tileList, Action callback = null)
    {
        TileAnimationBatch tileAnimationBatch = new TileAnimationBatch(tileList, callback);

        tileAnimationBatch.PlayCollectAnimation(delayPerTile);

        tileAnimationBatch.AnimationBatchFinishedEvent += OnAnimationBatchfinished;

        animationBatches.Add(tileAnimationBatch);
    }

    public void PlayCollectLastStandingTilesAnimation(List<GameObject> tileList, Action callback = null)
    {
        TileAnimationBatch tileAnimationBatch = new TileAnimationBatch(tileList, callback);

        tileAnimationBatch.PlayCollectLastStandingTilesAnimation(delayPerTile, GetCurrentBatchDelay());

        tileAnimationBatch.AnimationBatchFinishedEvent += OnAnimationBatchfinished;

        animationBatches.Add(tileAnimationBatch);
    }

    private void OnAnimationBatchfinished(TileAnimationBatch tileAnimationBatch)
    {
        tileAnimationBatch.AnimationBatchFinishedEvent -= OnAnimationBatchfinished;

        animationBatches.Remove(tileAnimationBatch);
    }
}