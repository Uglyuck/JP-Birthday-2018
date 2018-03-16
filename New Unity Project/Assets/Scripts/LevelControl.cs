using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {

	//public string NextLevel = "";
	public void LoadLevel(string name)
	{
		print("Loading level:" + name);
		SceneManager.LoadScene(name);
	}
}
