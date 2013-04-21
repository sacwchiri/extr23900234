using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProfileView : MonoBehaviour 
{	
	public Transform profileThing;
	
	public List<Texture> textu = new List<Texture>();
	
	private Ray r;
	private RaycastHit hit;
	
	void Start()
	{
		profileThing.gameObject.SetActive(false);
		hit =  new RaycastHit();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out hit))
			{
				switch(hit.transform.name)
				{
				case "1_Lava":
					profileThing.renderer.material.mainTexture = textu[5];
					profileThing.gameObject.SetActive(true);
					break;
				case "2_Salt":
					profileThing.renderer.material.mainTexture = textu[4];
					profileThing.gameObject.SetActive(true);
					break;
				case "3_Acid":
					profileThing.renderer.material.mainTexture = textu[1];
					profileThing.gameObject.SetActive(true);
					break;
				case "4_Radio":
					profileThing.renderer.material.mainTexture = textu[0];
					profileThing.gameObject.SetActive(true);
					break;
				case "5_Ice":
					profileThing.renderer.material.mainTexture = textu[2];
					profileThing.gameObject.SetActive(true);
					break;
				case "6_Desert":
					profileThing.renderer.material.mainTexture = textu[3];
					profileThing.gameObject.SetActive(true);
					break;
				default:
					break;
				}
			}
		}
	}
}
