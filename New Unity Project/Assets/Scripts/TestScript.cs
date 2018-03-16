using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour 
{
	private delegate Vector3 StatePointer();
	private StatePointer MoveForm;
	private Vector3 deltas;
	private Vector3 SaveVector;
	private float increment = 5f;
	private bool moving = false;
	private bool shouldMove = false;
	private float slowDown = 0;
	private float NewDistance;

	// Use this for initialization
	void Start () 
	{
		deltas = new Vector3();
		MoveForm = GetMousePosition;
	}

	// Update is called once per frame
	void Update()
	{
		//ElectricMove();
		
		Vector3 newPosition = new Vector3();
		Vector3 newRotation = new Vector3();
		if (Input.GetMouseButtonDown(0))
		{
			moving = true;
			shouldMove = true;
			SaveVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			slowDown = SaveVector.x - ((SaveVector.x - this.transform.position.x) / 4);
			NewDistance = (SaveVector.x - this.transform.position.x);
			//print("Distance: " + NewDistance);
			increment = Mathf.Abs(NewDistance) * 2;
			MoveForm = GetMousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			shouldMove = false;
			SaveVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			slowDown = SaveVector.x - ((SaveVector.x - this.transform.position.x) / 4);
			NewDistance = (SaveVector.x - this.transform.position.x);
			//print("Position Let Go: " + SaveVector.x);
			increment = Mathf.Abs(NewDistance) * 2;
			MoveForm = GetSavedPosition;
		}
		if (moving && increment > 1)
		{
			Vector3 MousePosition = MoveForm(); // Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (MousePosition.x - this.transform.position.x < 0)
			{
				newRotation.z = 25;
				if (this.transform.position.x < slowDown)
				{
					deltas.x = (increment * .5f) * Time.deltaTime * -1;
					newRotation.z = 5;
				}
				else
					deltas.x = (increment) * Time.deltaTime * -1;
			}
			else if(MousePosition.x - this.transform.position.x > 0)
			{
				newRotation.z = -25;
				if (this.transform.position.x > slowDown)
				{
					newRotation.z = -5;
					deltas.x = (increment * .5f) * Time.deltaTime;
				}
				else
					deltas.x = (increment) * Time.deltaTime;
			}

			if (Mathf.Abs(this.transform.position.x - MousePosition.x) < 0.2)
			{
				newPosition = this.transform.position;
				newPosition.x = MousePosition.x;
				increment = 0;
				if(!shouldMove)
				{
					moving = false;
				}
			}
			else
				newPosition = this.transform.position + deltas;

			this.transform.position = newPosition;
			
		}
		else if (moving)
		{
			newPosition = this.transform.position;
			newPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			this.transform.position = newPosition;
			if (!shouldMove)
			{
				moving = false;
			}
		}
		this.transform.eulerAngles = newRotation;
	}

	private Vector3 GetMousePosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private Vector3 GetSavedPosition()
	{
		return SaveVector;
	}

	private void ElectricMove()
	{
		Vector3 newPosition = new Vector3();
		if (Input.GetMouseButtonDown(0))
		{

			moving = true;
			slowDown = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - this.transform.position.x) / 4);
			//print("Slow down:" + slowDown);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			moving = false;

		}
		if (moving)
		{
			if (Mathf.Abs(deltas.x) < .01f)
			{
				//print("Reset SlowDown");
				slowDown = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - ((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - this.transform.position.x) / 4);
			}
			Vector3 MousePosition = new Vector3();
			MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			MousePosition.z = 0;
			if (MousePosition.x - this.transform.position.x < 0)
			{
				if (this.transform.position.x < slowDown)
				{
					deltas.x += (increment * .5f) * Time.deltaTime;
				}
				else
					deltas.x -= (increment) * Time.deltaTime;
			}
			else
			{
				if (this.transform.position.x > slowDown)
				{
					deltas.x -= (increment * .5f) * Time.deltaTime;
				}
				else
					deltas.x += (increment) * Time.deltaTime;
			}
		}
		else
		{
			if (deltas.x != 0)
			{
				if (deltas.x > 0)
				{
					float newX = deltas.x - (increment * Time.deltaTime);
					if (newX < 0)
					{
						//print("Too Low");
						newX = 0;
					}
					deltas.x = newX;
				}
				else if (deltas.x < 0)
				{
					float newX = deltas.x + (increment * Time.deltaTime);
					if (0 > newX)
						newX = 0;
					deltas.x = newX;
				}
				//print("DeltaX = " + deltas.x);
			}

		}
		newPosition = this.transform.position;
		newPosition.x += deltas.x;
		this.transform.position = newPosition;

	}
}
