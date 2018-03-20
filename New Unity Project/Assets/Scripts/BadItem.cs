using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Named bad because I was thinking of doing 2 of them... though realized it would be easier to just do 1 of them and just give it the same class.
public class BadItem : Stopable
{
	// Kill it at the end.
	void Update () 
	{
		moveSpeed(speed);
		if (this.transform.position.x < -50f)
			killMe();
	}

	// Move the object at specific speed
	void moveSpeed(float ItemSpeed)
	{
		Vector2 newPosition = this.transform.position;
		newPosition.x -= (Time.deltaTime * ItemSpeed);
		this.transform.position = newPosition;
	}

	// Makes it never-ending!!!!
	void killMe()
	{
		Vector2 NewPosition = new Vector2(130, (Random.RandomRange((int)0, (int)3) * 7) - 3 );
		this.transform.position = NewPosition;
	}

	// Could have done 2 objects or done another pointer to a function for this. But I figured a couple added lines of complexity
	// with an if statement wouldn't hurt given how light this was.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// When something good happens always make it more possible for shit to happen. :-)
		if(this.CompareTag("Good Item"))
		{
			Vector2 NewPosition = new Vector2(100, (Random.RandomRange((int)0, (int)3) * 7) - 3);
			GameObject go = Instantiate(GameObject.Find("Shit"));
			go.transform.position = NewPosition;
		}
		killMe();
	}
}
