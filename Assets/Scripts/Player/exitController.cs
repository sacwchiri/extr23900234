using UnityEngine;
using System.Collections;

public class exitController : MonoBehaviour {
	
	public Transform stageExit;
	public AudioSource exitGood;
	public Transform CharactersContainer;
	
	private byte ActiveCharacters = 0;
	private byte FinishedCharacters = 0;
	private bool playOnce = true;
	
	void Start()
	{
		ActiveCharacters = (byte)CharactersContainer.childCount;
//		Debug.Log(ActiveCharacters);
	}
	void Update()
	{
		if(FinishedCharacters >= ActiveCharacters)
		{
//			Debug.Log("Game is done");
			if(PlayerPrefs.HasKey("currentLevel"))
			{
				if(playOnce)
				{
					playOnce = false;
					PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);
					StartCoroutine(LoadNext());
				}
			}
			else
			{
				Debug.Log("No Key for current Level was found");
			}
		}
	}
	public IEnumerator LoadNext()
	{
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel("SelectLevelLoader");
	}
	void OnTriggerEnter(Collider c)
	{
		//play sound
		exitGood.Play(35000);
//		Debug.Log("Working");
	}
	
	public IEnumerator exitSecuence(Player player, PlayerMovement movement)
	{
		FinishedCharacters++;
		yield return StartCoroutine(player.happyDance());
		yield return StartCoroutine(movement.movePlayer(stageExit));
	}
}
