using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerLoader : MonoBehaviour {
	
	private SortedList<int,Transform> players = new SortedList<int, Transform>();
	private Transform[] temp;
	
	public bool moving = false;
	public bool activity = false;
	// Use this for initialization
	void Start () 
	{
		temp = transform.GetComponentsInChildren<Transform>();
		foreach(Transform t in temp)
		{
			if(t.name != "players")
			{
				players.Add(int.Parse(t.name.Split("_".ToCharArray())[0]),t);
				t.gameObject.SetActive(false);
			}
		}
		players[1].gameObject.SetActive(true);
		StartCoroutine(players[1].GetComponent<SimpleSpritePro1>().PlayAnimation(0));
		if(PlayerPrefs.GetInt("currentLevel") > 1)
		{
			for(int i = 2; i <= PlayerPrefs.GetInt("currentLevel");i++)
			{
				players[i].gameObject.SetActive(true);
				StartCoroutine(players[i].GetComponent<SimpleSpritePro1>().PlayAnimation(0));
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(moving && !activity)
		{
			activity = true;
			if(PlayerPrefs.GetInt("currentLevel") == 0)
			{
				players[1].GetComponent<SimpleSpritePro1>().ChangeAnimation(2);
			}
			else
			{
				for(int i = 1; i <= PlayerPrefs.GetInt("currentLevel");i++)
				{
					players[i].GetComponent<SimpleSpritePro1>().ChangeAnimation(2);
				}
			}
		}
		else if (!moving && activity)
		{
			activity = false;
			if(PlayerPrefs.GetInt("currentLevel") == 0)
			{
				players[1].GetComponent<SimpleSpritePro1>().ChangeAnimation(0);
			}
			else
			{
				for(int i = 1; i <= PlayerPrefs.GetInt("currentLevel");i++)
				{
					players[i].GetComponent<SimpleSpritePro1>().ChangeAnimation(0);
				}
			}
		}
	}
}
