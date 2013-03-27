using UnityEngine;
using System.Collections;

public class TriggerCell : MonoBehaviour {
	
	public GridMovement movement;
	
	private Collider currentObject;
	private Vector3 currentPos;
	private Vector3 copyPos;
	
	void OnTriggerEnter(Collider coll)
	{
		Transform child = transform.GetChild(0);
		//Debug.Log(child.position);
		currentPos = child.position;
		copyPos = currentPos;
		
		currentPos.x = 5.5f;
		child.position = currentPos;
		
		if(currentObject != null)
		{
			currentObject.transform.position = copyPos;
			movement.updateMovingCells();
		}
		currentObject = coll;
		child.renderer.material = currentObject.renderer.material;
	}
}
