using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// includes stopable incase I wanted to stop everything at any point of time.
public class Background : Stopable
{
	
	// Moving the object over each frame to create animation.
	void Update () 
	{
		Vector3 NewPosition = this.transform.position;
		if (NewPosition.x < -25)	// If it is beyond the screen.....
		{
			NewPosition.x += 50;	//Jump over to the end!
		}
		NewPosition.x -= (speed * Time.deltaTime);	//Move the object this much
		this.transform.position = NewPosition;		//Move it.
	}
}
