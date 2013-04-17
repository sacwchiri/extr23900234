using UnityEngine;
using System.Collections;

public class exitController : MonoBehaviour {
	
	public Transform stageExit;
	public AudioSource exitGood;
	public Transform CharactersContainer;
	
	private byte ActiveCharacters = 0;
	private byte FinishedCharacters = 0;
	
	void Start()
	{
		ActiveCharacters = (byte)CharactersContainer.childCount;
		Debug.Log(ActiveCharacters);
	}
	void Update()
	{
		if(FinishedCharacters >= ActiveCharacters)
		{
			Debug.Log("Game is done");
		}
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
