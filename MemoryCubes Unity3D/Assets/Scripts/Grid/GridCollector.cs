using UnityEngine;
using System;

public class GridCollector : MonoBehaviour
{
    public static event Action CollectEvent;
    
	public void OnCollectPressed()
    {        
        if (CollectEvent != null)
        {
            CollectEvent();
        }
    }
}
