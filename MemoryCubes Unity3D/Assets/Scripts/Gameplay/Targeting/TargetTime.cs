using UnityEngine;
using System.Collections;

public class TargetTime : MonoBehaviour
{
    [SerializeField] private float maxTime = 5f;

    [SerializeField] private float scalePerLevel = 1f;

    [SerializeField] private Builder builder;

    private float currentTime = 0f;
    
    private bool isActive = false;

	// Use this for initialization
	void Start ()
    {
        currentTime = maxTime;

        builder.BuilderReadyEvent += OnBuilderReady;
	}

    private void OnBuilderReady(object sender, BuilderReadyEventArgs e)
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isActive)
        {
            return;
        }

        UpdateCurrentTime();
	}

    private void UpdateCurrentTime()
    {
        currentTime -= Time.deltaTime;
    }
}
