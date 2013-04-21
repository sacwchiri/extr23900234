using UnityEngine;
using System.Collections;

public class clickKillTut : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			
			if(Physics.Raycast(r, out hit))
			{
				if(hit.transform.tag == "Button")
				{
					GameObject.Destroy(gameObject);
				}
			}
		}
	}
	
}
