using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour 
{
	public void Start()
	{	
		/*
		unsafe
		{
			print(sizeof(ameObject.transform));
		}
		*/
	}
	// Could have made this static. But it's light and I wanted to get it out quickly.
	public void LoadLevel(string name)
	{
		print("Loading level:" + name);
		SceneManager.LoadScene(name);
	}
}
