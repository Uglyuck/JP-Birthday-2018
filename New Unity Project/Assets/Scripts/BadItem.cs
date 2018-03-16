using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadItem : Stopable
{
	

	//private float speed = 15f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		moveSpeed(speed);
		if (this.transform.position.x < -50f)
			killMe();
	}
	void moveSpeed(float ItemSpeed)
	{
		Vector2 newPosition = this.transform.position;
		newPosition.x -= (Time.deltaTime * ItemSpeed);
		this.transform.position = newPosition;
	}
	void killMe()
	{
		Vector2 NewPosition = new Vector2(130, (Random.RandomRange((int)0, (int)3) * 7) - 3 );
		this.transform.position = NewPosition;
		//Destroy(gameObject);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(this.CompareTag("Good Item"))
		{
			Vector2 NewPosition = new Vector2(100, (Random.RandomRange((int)0, (int)3) * 7) - 3);
			GameObject go = Instantiate(GameObject.Find("Shit"));
			go.transform.position = NewPosition;
		}
		killMe();
	}
}
