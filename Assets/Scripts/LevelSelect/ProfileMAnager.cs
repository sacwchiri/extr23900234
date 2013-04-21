using UnityEngine;
using System.Collections;

public class ProfileMAnager : MonoBehaviour {
	
	Ray r;
	RaycastHit hit;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out hit))
			{
				if(hit.transform.tag == "Planet")
				{
					gameObject.SetActive(false);
				}
			}
		}
	}
}
