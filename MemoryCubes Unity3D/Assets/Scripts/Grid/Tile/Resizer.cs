using UnityEngine;

public class Resizer : MonoBehaviour 
{
	private SelectionState selectionState = new SelectionState();
	
	private float originScale;
	
	private float targetScale;
	
	private Vector3 scalingVelocity = Vector3.zero;
	
	private Deadzone deadzone = new Deadzone();
	
	[SerializeField] private float selectedScale = 0.5f;
	
	[SerializeField] private float scalingSmoothTime = 1f;
	
	[SerializeField] private float maxScalingSpeed = 1f;
	
	// Use this for initialization
	void Start() 
	{
		// We want to scale all axes by the same amount, so using scale.x is OK
		originScale = transform.localScale.x;
		
		targetScale = originScale;
		
		Selector selector = GetComponent<Selector>();
		
		selector.SelectEvent += OnSelectEvent;
	}
	
	// Update is called once per frame
	void Update() 
	{
		Vector3 scale = new Vector3(targetScale, targetScale, targetScale);
		
		if (deadzone.OutOfReach(transform.localScale, scale))
		{
			transform.localScale = Vector3.SmoothDamp(transform.localScale, scale, ref scalingVelocity, scalingSmoothTime, maxScalingSpeed);
		}
	}
	
	private void OnSelectEvent(object sender, SelectorArgs e)
	{
		selectionState = e.selectionState;
		
		SetTargetScale();
	}
	
	private void SetTargetScale()
	{
		if (selectionState == SelectionState.selected)
		{
			targetScale = originScale * selectedScale;
		}
		else
		{
			targetScale = originScale;
		}
	}
}
