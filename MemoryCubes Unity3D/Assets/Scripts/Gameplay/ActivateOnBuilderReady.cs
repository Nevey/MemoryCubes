using UnityEngine;
using System.Collections;

public class ActivateOnBuilderReady : MonoBehaviour
{
    [SerializeField] Builder builder;

	// Use this for initialization
	void Start()
    {
        gameObject.SetActive(false);

        builder.BuilderReadyEvent += OnBuilderReady;
	}

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        gameObject.SetActive(true);
    }
}
