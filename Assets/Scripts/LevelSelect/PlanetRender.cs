using UnityEngine;
using System.Collections;

public class PlanetRender : MonoBehaviour {
	
	public Texture[] planetTextures;
	private int currLevel;
	// Use this for initialization
	void Start ()
	{
		currLevel = PlayerPrefs.GetInt("currentLevel");
		transform.renderer.material.mainTexture = planetTextures[currLevel];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
