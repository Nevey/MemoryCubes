using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Rename to ParticlesController
public class ParticlesSpawner : MonoBehaviour
{
	[SerializeField] private ParticleSystem particlesPrefab;

	[SerializeField] private float lifeSpan;

	private List<ParticleSystem> particlesList = new List<ParticleSystem>();

	private MaterialPropertyBlock propertyBlock;

	private void Start()
	{
		propertyBlock = new MaterialPropertyBlock();
	}

	private void SetColor(GameObject particlesGO, Color color)
	{
		propertyBlock.SetColor("_Color", color);

		Renderer renderer = particlesGO.GetComponent<Renderer>();

		renderer.SetPropertyBlock(propertyBlock);
	}

	/// <summary>
	/// Spawn particles on parent and position of target.
	/// </summary>
	/// <param name="target"></param>
	/// <returns></returns>
	public ParticleSystem Spawn(Transform target, bool scaleWithTarget = true)
	{
		// Create particles gameobject
		GameObject particlesGO = Instantiate(particlesPrefab.gameObject);

		particlesGO.transform.parent = target.parent;

		particlesGO.transform.localPosition = target.localPosition;

		particlesGO.transform.localScale = target.localScale;

		// Add particle system to list
		ParticleSystem particleSystem = particlesGO.GetComponent<ParticleSystem>();		

		particlesList.Add(particleSystem);

		// Override particle lifespan
		ParticleSystem.MainModule mainModule = particleSystem.main;

		mainModule.startLifetime = lifeSpan;

		// Return...
		return particleSystem;
	}

	/// <summary>
	/// Spawn particles on parent and position of target.
	/// Spawn particles with custom color (WIP!).
	/// </summary>
	/// <param name="target"></param>
	/// <param name="color"></param>
	public void Spawn(Transform target, Color color, bool scaleWithTarget = true)
	{
		ParticleSystem particleSystem = Spawn(target, scaleWithTarget);

		SetColor(particleSystem.gameObject, color);
	}
}
