using UnityEngine;
using System.Collections;

public class LevelSelectLoader : MonoBehaviour {
	
	public string levelName;
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(runIt());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private IEnumerator runIt()
	{
		yield return new WaitForSeconds(2);
		Application.LoadLevel(levelName);
	}
}
