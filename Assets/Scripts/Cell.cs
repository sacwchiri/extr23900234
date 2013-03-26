using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public Vector2 pos;
	// Use this for initialization
	void Start () 
	{
		pos.x = transform.position.x;
		pos.y = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
