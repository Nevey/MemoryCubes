using UnityEngine;

public class Deadzone
{
	float defaultDeadzone = 0.001f;
	
	public bool InReach(Vector3 a, Vector3 b)
	{
		float distance = Vector3.Distance(a, b);
		
		if (distance < defaultDeadzone)
			return true;
		else 
			return false;
	}

    public bool InReach(Vector3 a, Vector3 b, float deadZone)
    {
        float distance = Vector3.Distance(a, b);

        if (distance < deadZone)
            return true;
        else
            return false;
    }

	public bool InReach(Vector4 a, Vector4 b)
	{
		float distance = Vector4.Distance(a, b);
		
		if (distance < defaultDeadzone)
			return true;
		else 
			return false;
	}

	public bool InReach(Vector4 a, Vector4 b, float deadZone)
    {
        float distance = Vector4.Distance(a, b);

        if (distance < deadZone)
            return true;
        else
            return false;
    }

	public bool InReach(Color a, Color b)
	{
		float distance = Vector4.Distance(a, b);
		
		if (distance < defaultDeadzone)
			return true;
		else 
			return false;
	}

	public bool InReach(Color a, Color b, float deadZone)
    {
        float distance = Vector4.Distance(a, b);

        if (distance < deadZone)
            return true;
        else
            return false;
    }
	
	public bool InReach(float a, float b)
	{
		float distance = b - a;
		
		if (distance > 0 && distance < defaultDeadzone ||
			distance < 0 && distance > -defaultDeadzone)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool InReach(float a, float b, float deadZone)
	{
		float distance = b - a;
		
		if (distance > 0 && distance < deadZone ||
			distance < 0 && distance > -deadZone)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

    public bool OutOfReach(Vector3 a, Vector3 b)
    {
        float distance = Vector3.Distance(a, b);

        if (distance > defaultDeadzone)
            return true;
        else
            return false;
    }
	
	public bool OutOfReach(Vector3 a, Vector3 b, float deadZone)
	{
		float distance = Vector3.Distance(a, b);
		
		if (distance > deadZone)
			return true;
		else 
			return false;
	}

	public bool OutOfReach(Vector4 a, Vector4 b)
    {
        float distance = Vector4.Distance(a, b);

        if (distance > defaultDeadzone)
            return true;
        else
            return false;
    }

	public bool OutOfReach(Vector4 a, Vector4 b, float deadZone)
	{
		float distance = Vector4.Distance(a, b);
		
		if (distance > deadZone)
			return true;
		else 
			return false;
	}
	
	public bool OutOfReach(float a, float b)
	{
		float distance = b - a;
		
		if (distance > 0 && distance > defaultDeadzone ||
			distance < 0 && distance < -defaultDeadzone)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool OutOfReach(float a, float b, float deadZone)
	{
		float distance = b - a;
		
		if (distance > 0 && distance > deadZone ||
			distance < 0 && distance < -deadZone)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}