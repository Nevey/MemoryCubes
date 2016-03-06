using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour
{
	private Vector3 originPosition = Vector3.zero;
	
	private Vector3 targetPosition = Vector3.zero;
	
	private Vector3 floatVelocity = Vector3.zero;
	
	private Deadzone deadzone = new Deadzone();
	
	private enum FloatState
	{
		idle,
		floating
	};
	
	private FloatState floatState = new FloatState();
	
	public float maxFloatDistance = 1f;
	
	public float movementSmoothTime = 1f;
	
	public float maxMovementSpeed = 1f;
	
	public float minWaitTime = 2f;
	
	public float maxWaitTime = 5f;
	
	public bool singleAxisMovement = false;
	
	public bool restrainX = false;
	
	public bool restrainY = false;
	
	public bool restrainZ = false;
	
    void Start()
	{
		// Setup the origin position
		originPosition = transform.localPosition;
		
		StartCoroutine(Wait());
	}

	void Update()
	{
		UpdatePosition();
	}
	
	private void UpdatePosition()
	{
		if (floatState == FloatState.idle)
		{
			return;
		}
		
		if (deadzone.OutOfReach(transform.localPosition, targetPosition))
		{
			transform.localPosition = Vector3.SmoothDamp(
				transform.localPosition, 
				targetPosition, 
				ref floatVelocity, 
				movementSmoothTime, 
				maxMovementSpeed);
		}
		else
		{
			StartCoroutine(Wait());
		}
	}
	
	private IEnumerator Wait()
	{
		floatState = FloatState.idle;
		
		float waitTime = Random.Range(minWaitTime, maxWaitTime);
		
		yield return new WaitForSeconds(waitTime);
		
		SetTargetPosition();
	}
	
	private void SetTargetPosition()
	{
		targetPosition = new Vector3(
			originPosition.x + Random.Range(-maxFloatDistance, maxFloatDistance),
			originPosition.y + Random.Range(-maxFloatDistance, maxFloatDistance),
			originPosition.z + Random.Range(-maxFloatDistance, maxFloatDistance)
		);
		
		SetupSingleAxisMovement();
		
		RestrainAxes();
		
		floatState = FloatState.floating;
	}
	
	private void SetupSingleAxisMovement()
	{
		if (singleAxisMovement)
		{
			int random = Random.Range(0, 3);
			
			switch (random)
			{
				// Only move on x axis
				case 0:
					
					targetPosition.y = transform.localPosition.y;
					targetPosition.z = transform.localPosition.z;
					
				break;
				
				// Only move on y axis
				case 1:
				
					targetPosition.x = transform.localPosition.x;
					targetPosition.z = transform.localPosition.z;
					
				break;
				
				// Only move on z axis
				case 2:
					
					targetPosition.x = transform.localPosition.x;
					targetPosition.y = transform.localPosition.y;
					
				break;
			}
		}
	}
	
	private void RestrainAxes()
	{
		if (restrainX)
		{
			targetPosition.x = originPosition.x;
		}
		
		if (restrainY)
		{
			targetPosition.y = originPosition.y;
		}
		
		if (restrainZ)
		{
			targetPosition.z = originPosition.z;
		}
	}
}