using UnityEngine;
using System.Collections;

public class loadConicytLoader : MonoBehaviour {

	void Start () 
	{
		StartCoroutine(Fader());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private IEnumerator Fader ()
	{
		yield return StartCoroutine(TextureFade(renderer.material, 0.5f));
		yield return new WaitForSeconds(2);
		Application.LoadLevel("introConicitLoad");
	}
	
	private IEnumerator TextureFade(Material mat, float rate)
	{
		float r,g,b,a;
		r = g = b = 1;
		a = 0;
		mat.color = new Color(r,g,b,a);
		while(a < 1)
		{
			a = Mathf.MoveTowards(a, 1, rate * Time.deltaTime);
			mat.color = new Color(r,g,b,a);
			yield return null;
		}
	}
}
