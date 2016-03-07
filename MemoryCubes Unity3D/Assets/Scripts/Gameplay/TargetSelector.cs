using UnityEngine;
using System;

public enum TargetColors
{
    Blue,
    Green,
    Purple,
    Red,
    Yellow
}

public class NextTargetEventArgs : EventArgs
{
    public TargetColors targetColor { get; set; }
}

public class TargetSelector : MonoBehaviour {

    public event EventHandler<NextTargetEventArgs> NextTargetEvent;

	// Use this for initialization
	void Start() 
    {
        GameObject gridParent = GameObject.Find("GridParent");
        
        gridParent.GetComponent<Builder>().BuilderReadyEvent += OnBuilderReadyEvent;
        
        gridParent.GetComponent<GridCollector>().CollectEvent += OnCollectEvent;
	}
    
    private void OnBuilderReadyEvent(object sender, BuilderReadyEventArgs e)
    {
        SetNextTarget();
    }
    
    private void OnCollectEvent(object sender, CollectEventArgs e)
    {
        SetNextTarget();
    }
    
    private void SetNextTarget()
    {
        NextTargetEventArgs args = new NextTargetEventArgs();
        
        GameObject randomCube = GetRandomCube();
        
        args.targetColor = GetTargetColor(randomCube);
        
        Debug.Log("New target set: " + args.targetColor);
        
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
    
    private TargetColors GetTargetColor(GameObject randomCube)
    {
        TargetColors targetColor = new TargetColors();
        
        Material cubeMaterial = randomCube.GetComponent<Renderer>().material;
        
        string[] materialNameSplitted = cubeMaterial.name.Split(' ');
        
        string materialName = materialNameSplitted[0];
        
        targetColor = (TargetColors)Enum.Parse(typeof(TargetColors), materialName);
        
        return targetColor;
    }
}
