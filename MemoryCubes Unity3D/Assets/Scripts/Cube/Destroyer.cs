using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private Selector selector;
    
    private GameObject gridParent;

	// Use this for initialization
	void Awake() 
    {
        gridParent = GameObject.Find("GridParent");
	}
    
    void OnEnable()
    {
        PlayerCollectingCubesState.CollectingCubesStateStartedEvent += OnCollectingCubesStateStarted;
    }

    void OnDisable()
    {
        PlayerCollectingCubesState.CollectingCubesStateStartedEvent -= OnCollectingCubesStateStarted;
    }

    private void OnCollectingCubesStateStarted()
    {
        if (selector.CurrentSelection == SelectionState.selected)
        {
            Destroy(this.gameObject);
        }
    }
}
