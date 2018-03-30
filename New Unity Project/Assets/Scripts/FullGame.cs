using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Note: Due to the difficulty of testing and the unknowingness of the iPhone (target device.) 
 *			And the target Audience (person who doesn't want to install apps on his phone.) 
 *		I mocked up something quickly and tried to test it in the short amount of time I had. 
 *			
 *			Given more time I would resize all of the sprites to use the same resolution and size. 
 *				(This wasn't done due to time and effort put in to try to find a device to test on. 
 *					And emulators didn't seem to have the same result as the physical device.)
 *			
 *			If I wanted to really optomize it for over 200 objects I would:
 *			
 *			1. Move it from OO style to DO style to optomize memory fetching and optomize for reusing memory in the CPU.
 *				a. Move it to a 2d array of x values for each item
 *				b. 3 sub arrays for all the 3 possibilities.
 *				c. incriment all of the values by the time distance.
 *				d. Check for collision in only the arrays where his vertical position is.
 *				e. Create a variable for gravity to run the main character and create a colision based on the lowest y value then reset the gravity variable (Since gravity is figured already manually)
 *			
 *			
 *			This would remove the need for all rigid bodies and lessen the number of CS files to about 2. 
 *			But given the size of this it's rather impractacle to make that kind of adjustment to the code.
 * 
 * 
 *		Michael Pelz
 * */



// This is just a little placeholder for the timer and the functions of the overall game itself.
// Pretty simple
public class FullGame : MonoBehaviour 
{
	public float TimeRemaining;		//Ajustable in the object in the Interface.

	public Text TimerSeconds;		//This is for the display 
	public Text TimerMiliSeconds;   //This is for the display


	private float deltaTime;
	private float TotalClock;
	private int Cycles;
	private System.Diagnostics.Stopwatch timer;
	private float TotalTime = 0;

	// Use this for initialization
	void Start () 
	{
		timer = System.Diagnostics.Stopwatch.StartNew();
	}

	// Update is called once per frame
	void Update()
	{

		deltaTime = Time.deltaTime;
		TimeRemaining -= deltaTime;
		if (TimeRemaining <= 0)
		{
			win();
		}
		TimerSeconds.text = Mathf.Floor(TimeRemaining).ToString("00"); // Round down then cast
		TimerMiliSeconds.text = ((TimeRemaining % 1) * 100).ToString("00"); // Mod 1 to keep it at under 1 second then multiply for miliseconds.


		// Calc Performance;
		TotalClock += deltaTime;
		//Cycles++;
		if (TotalClock > 5)
		{
			print("Average Cycle time:" + TotalClock / Cycles);
			Cycles = 0;
			TotalClock = 0;
		}
		if (timer.IsRunning)
		{
			timer.Stop();
			System.TimeSpan ts = timer.Elapsed;
			TotalTime += timer.ElapsedTicks;
			Cycles++;
			if (Cycles % 120 == 0)
			{
				print("Average Ticks In WholeThing:" + TotalTime / Cycles);
				TotalTime = 0;
				Cycles = 0;
			}
			timer = System.Diagnostics.Stopwatch.StartNew();
		}

	}
	void win()
	{
		this.GetComponent<LevelControl>().LoadLevel("WinScene");
	}
}
