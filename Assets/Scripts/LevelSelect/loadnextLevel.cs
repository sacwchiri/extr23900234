using UnityEngine;
using System.Collections;

public class loadnextLevel : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Application.LoadLevel("Level"+PlayerPrefs.GetInt("currentLevel"));
	}
}
