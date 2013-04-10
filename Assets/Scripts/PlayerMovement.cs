using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	
	public bool check = false;
	
	List<RaycastHit> surroundings = new List<RaycastHit>();
	
	RaycastHit temp;
	Vector3 origin;
	Vector3 direction;
	
	public Cell.CellType playerType;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	// Update is called once per frame
	void Update () 
	{
		if(check)
		{
			origin = transform.position;
			direction = Vector3.forward;
			Physics.Raycast(origin,direction,out temp);
			surroundings.Add(temp);
		}
	}
}
