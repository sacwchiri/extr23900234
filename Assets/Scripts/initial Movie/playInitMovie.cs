using UnityEngine;
using System.Collections;

public class playInitMovie : MonoBehaviour {

//	private AudioSource theme;
	public GameObject[] scenes;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(movieManager());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	 

	private IEnumerator movieManager ()
	{
		scenes[0].SetActive(true);
		yield return new WaitForSeconds(1.1f);
		scenes[1].SetActive(true);
		yield return new WaitForSeconds(0.9f);
		scenes[2].SetActive(true);
		yield return new WaitForSeconds(0.9f);
		scenes[0].SetActive(false);
		scenes[1].SetActive(false);
		scenes[2].SetActive(false);
		scenes[3].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		scenes[4].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		scenes[5].SetActive(true);
		yield return new WaitForSeconds(1.8f);
		scenes[6].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		scenes[7].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		scenes[8].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		scenes[9].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		scenes[10].SetActive(true);
		yield return new WaitForSeconds(1.5f);
		
		foreach(GameObject r in scenes)
		{
			r.SetActive(false);
		}
		Application.LoadLevel("loadLevel");
	}
}
