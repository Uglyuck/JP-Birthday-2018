using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stopable : MonoBehaviour 
{
	public float speed = 15f;
	public void stop()
	{
		speed = 0;
	}
}

