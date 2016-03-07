using UnityEngine;
using System;

public class NextTargetEventArgs : EventArgs
{
    public GameObject targetCube { get; set; }
}

public class TargetSelector : MonoBehaviour {

    public event EventHandler<NextTargetEventArgs> NextTargetEvent;

	// Use this for initialization
	void Start() 
    {
	   SetNextTarget();
	}
	
	// Update is called once per frame
	void Update() 
    {
	
	}
    
    private void SetNextTarget()
    {
        NextTargetEventArgs args = new NextTargetEventArgs();
        
        args.targetCube = GetRandomCube();
        
        if (NextTargetEvent != null)
        {
            NextTargetEvent(this, args);
        }
    }
    
    private GameObject GetRandomCube()
    {
        GameObject gridParent = GameObject.Find("GridParent");
        
        int randomIndex = UnityEngine.Random.Range(0, gridParent.transform.childCount - 1);
        
        GameObject cube = gridParent.transform.GetChild(randomIndex).gameObject;
        
        return cube;
    }
}
