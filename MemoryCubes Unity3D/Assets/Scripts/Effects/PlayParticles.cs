using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticles : MonoBehaviour
{
	[SerializeField] private GameObject particlesObjectPrefab;

	private GameObject particlesObject;

	private void ShowParticles(Vector3 position)
	{
		particlesObject = Instantiate(particlesObjectPrefab);

		particlesObject.transform.position = position;
	}
}
