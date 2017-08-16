using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void DestroyCube()
    {
        // TODO: change to DestroyerView and do fancy VFX in here
        Destroy(gameObject);

        // TODO: disable mesh renderer
        // TODO: play particles animation
        // TODO: after a short time, destroy gameObject
    }
}
