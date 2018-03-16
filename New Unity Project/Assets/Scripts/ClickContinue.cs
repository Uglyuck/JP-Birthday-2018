using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickContinue : MonoBehaviour 
{

	public GameObject NextScreen;
	public bool FirstScreen = false;
	public AudioSource LoadSound = null;
	// Use this for initialization
	private void Awake()
	{
		if (!FirstScreen)
		{
			this.gameObject.active = false;
			this.enabled = false;
			if (LoadSound != null)
			{

			}
		}
	}
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))	
		{
			this.enabled = false;
			if(NextScreen != null)
			{
				NextScreen.active = true;
				NextScreen.GetComponent<ClickContinue>().FirstScreen = true;
				NextScreen.GetComponent<ClickContinue>().enabled = true;
			}
			else
			{
				startGame();
			}
		}
	}
	void startGame()
	{
		//print("Starting Game");
	}
}
