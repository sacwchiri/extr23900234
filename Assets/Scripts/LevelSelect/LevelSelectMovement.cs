using UnityEngine;
using System.Collections;

public class LevelSelectMovement : MonoBehaviour {
	
	public Transform planet;
	public playerLoader ActivePlayers;
	
	Ray r;
	RaycastHit hit;
	// Use this for initialization
	void Start ()
	{
		hit = new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButton(0))
		{
			r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out hit))
			{
				if(hit.transform.tag == "Planet")
				{
					ActivePlayers.moving = true;
					planet.Rotate(Vector3.up * 15 * -Time.deltaTime,Space.Self);
				}
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			ActivePlayers.moving = false;
		}
	}
}
