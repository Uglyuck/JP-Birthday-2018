using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Created just incase I wanted to stop everything....
// (Was tempted to do this for when he got shit, but not jumping was punishment enough so he could see opportunities for gifts fly past him.)
public abstract class Stopable : MonoBehaviour
{
	public float speed = 15f;
	public void stop()
	{
		speed = 0;
	}
}