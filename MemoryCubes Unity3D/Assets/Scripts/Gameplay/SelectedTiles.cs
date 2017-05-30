using UnityEngine;
using System.Collections.Generic;

public class SelectedTiles : MonoBehaviour
{
    [SerializeField] private Builder builder;

    private List<GameObject> selectedCubesList = new List<GameObject>();

    public List<GameObject> SelectedCubesList
    {
        get { return selectedCubesList; }
    }

	// Use this for initialization
	void Start()
    {
        builder.BuilderReadyEvent += OnBuilderReady;

        DestroyController.DestroyFinishedEvent += OnDestroyFinished;
    }

    private void OnDestroyFinished()
    {
        ClearSelectedCubesList();
    }

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        // Start listening to selector behavior on all cubes

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            Selector selector = child.GetComponent<Selector>();

            if (selector == null)
            {
                continue;
            }

            selector.SelectEvent += OnCubeSelect;
        }
    }

    private void OnCubeSelect(object sender, SelectorArgs e)
    {
        switch (e.selectionState)
        {
            case SelectionState.selected:
                selectedCubesList.Add(e.selectedObject);
                break;

            case SelectionState.notSelected:
                selectedCubesList.Remove(e.selectedObject);
                break;
        }
    }

    private void ClearSelectedCubesList()
    {
        selectedCubesList.Clear();
    }
}
