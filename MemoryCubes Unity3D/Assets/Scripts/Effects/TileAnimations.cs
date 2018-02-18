using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.Base;

public class TileAnimations : MonoBehaviourSingleton<TileAnimations>
{
    [Header("Used when building the cube")]
    [SerializeField] private float buildDelayPerTile = 0.025f;

    [Header("Cube clearance values")]
    [SerializeField] private float resizeLastStandingDelayPerTile = 0.15f;

    [SerializeField] private float collectDelayPerTile = 0.15f;

    private List<TileAnimationBatch> animationBatches = new List<TileAnimationBatch>();

    private TileAnimationBatch CreateNewAnimationBatch(List<GameObject> tileList, Action callback = null)
    {
        TileAnimationBatch tileAnimationBatch = new TileAnimationBatch(tileList, callback);

        animationBatches.Add(tileAnimationBatch);

        tileAnimationBatch.AnimationBatchFinishedEvent += OnAnimationBatchfinished;

        return tileAnimationBatch;
    }

    public void PlayBuildAnimation(List<GameObject> tileList, Action callback = null)
	{
        List<GameObject> shuffledList = new List<GameObject>(tileList);

        shuffledList.Shuffle();

        CreateNewAnimationBatch(shuffledList, callback)
            .PlayBuildAnimation(buildDelayPerTile);
	}

    public void PlayCollectAnimation(List<GameObject> tileList, Action callback = null)
    {
        CreateNewAnimationBatch(tileList, callback)
            .PlayCollectAnimation(collectDelayPerTile);
    }

    public void PlayCollectLastStandingTilesAnimation(List<GameObject> tileList, Action callback = null)
    {
        CreateNewAnimationBatch(tileList, callback)
            .PlayCollectLastStandingTilesAnimation(resizeLastStandingDelayPerTile, collectDelayPerTile);
    }

    private void OnAnimationBatchfinished(TileAnimationBatch tileAnimationBatch)
    {
        tileAnimationBatch.AnimationBatchFinishedEvent -= OnAnimationBatchfinished;

        animationBatches.Remove(tileAnimationBatch);
    }
}