using UnityEngine;
using System.Collections;

public class holeHelp : MonoBehaviour {
	
	public Renderer helpSign;
	private bool updater = false;
	private float timer;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(updater)
		{
			if(timer > 0 && timer < 0.5f)
			{
				helpSign.enabled = true;
			}
			else if(timer > 0.5f)
			{
				helpSign.enabled = false;
				timer*=-1;
			}
			timer += Time.deltaTime;
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
		{
			timer =0;
			updater = true;
		}
	}
	
	void OnTriggerExit(Collider c)
	{
		if(c.tag == "Player")
		{
			helpSign.enabled = false;
			updater = false;
		}
			
	}
}
