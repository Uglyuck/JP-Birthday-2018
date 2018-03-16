using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : Stopable
{
	//private float speed = 6;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 NewPosition = this.transform.position;
		if (this.transform.position.x < -39)	
		{
			NewPosition.x += 72; // 36;	 //'33' -39
		}
		NewPosition.x -= (speed * Time.deltaTime);
		this.transform.position = NewPosition;
	}
}
