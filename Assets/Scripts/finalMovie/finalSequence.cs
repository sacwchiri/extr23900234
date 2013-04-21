using UnityEngine;
using System.Collections;

public class finalSequence : MonoBehaviour {

	public GameObject[] scenes;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(movieManager());
	}
	
	
	private IEnumerator movieManager ()
	{
		scenes[0].SetActive(true);
		yield return new WaitForSeconds(1.7f);
		scenes[1].SetActive(true);
		yield return new WaitForSeconds(1.7f);
		scenes[2].SetActive(true);
		yield return new WaitForSeconds(1.7f);
		scenes[3].SetActive(true);
		yield return new WaitForSeconds(1.7f);
		scenes[4].SetActive(true);
		yield return new WaitForSeconds(1.7f);//sumar 8.5
		scenes[5].SetActive(true);
		yield return new WaitForSeconds(2.24f);
		scenes[6].SetActive(true);
		yield return new WaitForSeconds(2.24f);//sumar 13
		scenes[7].SetActive(true);
		yield return new WaitForSeconds(4f);
		scenes[8].SetActive(true);
		yield return new WaitForSeconds(4.5f);
		
		Application.LoadLevel("InitialLoad");
	}
}
