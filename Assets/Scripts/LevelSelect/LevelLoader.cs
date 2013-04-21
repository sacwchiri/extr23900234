using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {
	
	public byte levelNumber;
	public Transform[] holes;
	public Transform LevelEntrance;
	
	void Start()
	{
		if(PlayerPrefs.GetInt("currentLevel") < 6)
		{
			LevelEntrance.position = holes[PlayerPrefs.GetInt("currentLevel")].position;
			LevelEntrance.rotation = holes[PlayerPrefs.GetInt("currentLevel")].rotation;
		}
		else
		{
			StartCoroutine(TriggerFinalMovie());
			LevelEntrance.position = Vector3.zero;;
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
			Application.LoadLevel("loadLevel");//go to level loader
		
	}

	private IEnumerator TriggerFinalMovie ()
	{
		yield return new WaitForSeconds(4);
		Application.LoadLevel("finalAnimationLoader");
	}
}
