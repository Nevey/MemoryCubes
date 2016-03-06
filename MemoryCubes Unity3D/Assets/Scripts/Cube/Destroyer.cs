using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    private Selector selector;

	// Use this for initialization
	void Start() 
    {
	   selector = this.GetComponent<Selector>();
       
       Debug.Log(selector);
	}
    
    public void DestroyMe()
    {
        selector = this.GetComponent<Selector>();
        Debug.Log(selector.CurrentSelection);
        
        if (selector.CurrentSelection == SelectionState.selected)
        {
            Destroy(this.gameObject);
        }
    }
}
