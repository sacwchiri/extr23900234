using UnityEngine;
using System.Collections;

public class definePlayerPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		PlayerPrefs.SetInt("currentLevel", 0);
		
		Application.LoadLevel("StartScreen");
	}
	
}
