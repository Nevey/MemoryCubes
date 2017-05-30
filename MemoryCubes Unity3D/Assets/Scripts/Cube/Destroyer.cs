using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    
	// Use this for initialization
	void Awake() 
    {
        
	}

    public void DestroyCube()
    {
        // TODO: change to DestroyerView and do fancy VFX in here
        Destroy(this.gameObject);
    }
}
