using UnityEngine;
using System;

public class CollectEventArgs : EventArgs
{
    
}

public class GridCollector : MonoBehaviour {

    public event EventHandler<CollectEventArgs> CollectEvent;

	public void OnCollectPressed()
    {
        CollectEventArgs args = new CollectEventArgs();
        
        if (CollectEvent != null)
        {
            CollectEvent(this, args);
        }
    }
}
