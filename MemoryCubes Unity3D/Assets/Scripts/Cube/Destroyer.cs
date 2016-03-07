using UnityEngine;

public class Destroyer : MonoBehaviour {

    private Selector selector;
    
    private GameObject gridParent;

	// Use this for initialization
	void Awake() 
    {
        gridParent = GameObject.Find("GridParent");
        
	    selector = this.GetComponent<Selector>();
	}
    
    void OnEnable()
    {
        gridParent.GetComponent<GridCollector>().CollectEvent += OnCollectEvent;
    }
    
    void OnDisable()
    {
        gridParent.GetComponent<GridCollector>().CollectEvent -= OnCollectEvent;
    }
    
    private void OnCollectEvent(object sender, CollectEventArgs e)
    {
        if (selector.CurrentSelection == SelectionState.selected)
        {
            Destroy(this.gameObject);
        }
    }
}
