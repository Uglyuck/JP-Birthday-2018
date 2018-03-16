using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullGame : MonoBehaviour 
{
	public float TimeRemaining;
	public Text TimerSeconds;
	public Text TimerMiliSeconds;
	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update()
	{
		TimeRemaining -= Time.deltaTime;
		if (TimeRemaining <= 0)
		{
			//GameObject[] allObjects = GameObject.FindObjectOfType<Stopable>();
			//print("FInished!");
			win();
		}
		//Timer.text = "00:";
		TimerSeconds.text = (TimeRemaining - .4).ToString("00");
		TimerMiliSeconds.text = ((TimeRemaining % 1) * 100).ToString("00");
	}
	void win()
	{
		this.GetComponent<LevelControl>().LoadLevel("WinScene");
	}
}
