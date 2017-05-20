using UnityEngine;
using System;

public class CollectEventArgs : EventArgs
{
    // TODO: list of collected cubes?
}

public class GridCollector : MonoBehaviour
{
    //public event EventHandler<CollectEventArgs> CollectEvent;

    public static event Action CollectEvent;

    // TODO: keep a list of collected cubes?

	public void OnCollectPressed()
    {
        CollectEventArgs args = new CollectEventArgs();
        
        if (CollectEvent != null)
        {
            CollectEvent();
        }
    }
}
