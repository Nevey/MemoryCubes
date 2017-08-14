using UnityEngine;
using System;

public class GridCollector : MonoBehaviour
{
    //public event EventHandler<CollectEventArgs> CollectEvent;

    public static event Action CollectEvent;

    // TODO: keep a list of collected cubes?

	public void OnCollectPressed()
    {        
        if (CollectEvent != null)
        {
            CollectEvent();
        }
    }
}
