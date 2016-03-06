using UnityEngine;

public class Destroyer : MonoBehaviour {

    private Selector selector;

	// Use this for initialization
	void Start() 
    {
       GameObject.Find("GridParent").GetComponent<GridCollector>().CollectEvent += OnCollectEvent;
        
	   selector = this.GetComponent<Selector>();
	}
    
    private void OnCollectEvent(object sender, CollectEventArgs e)
    {
        if (selector.CurrentSelection == SelectionState.selected)
        {
            Destroy(this.gameObject);
        }
    }
}
