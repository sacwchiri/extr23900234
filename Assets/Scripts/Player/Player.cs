using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool wrongCell = false;
	public float health = 100;
	
	public bool walking = false;
	public bool running = false;
	private bool[] runningAnimations = new bool[10];
	private SimpleSpritePro1 anima;
	private byte animationIndex = 0;
	
	// Use this for initialization
	void Start () 
	{
		anima = GetComponent<SimpleSpritePro1>();
		for(int i =0;i<10;i++)
		{
			runningAnimations[i] = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!walking)
		{
			if(running)
			{
				running = false;
				anima.ChangeAnimation(animationIndex);
			}
			normalManager();
		}
		else
		{
			if(!running)
			{
				running = true;
				anima.ChangeAnimation(2);
			}
		}
	}

	private void normalManager ()
	{
		if(wrongCell)
		{
			if(health > 0)
			{
				health -= 25 * Time.deltaTime;
				if(health < 0)
				{
					health = 0;
				}
			}
		}
		else
		{
			if(health < 100)
			{
				health += 25 * Time.deltaTime;
				if(health > 100)
				{
					health = 100;
				}
			}
		}
		switch((int)health/10)
		{
			case 1:
				break;
			case 2:
				break;
			case 3:
				if(runningAnimations[3])
				{
					animationIndex = 1;
					Debug.Log("inside");
					runningAnimations[3] = false;
					anima.ChangeAnimation(animationIndex);
					runningAnimations[4] = true;
				}
				break;
			case 4:
				if(runningAnimations[4])
				{
					animationIndex = 0;
					Debug.Log("inside");
					runningAnimations[4] = false;
					anima.ChangeAnimation(animationIndex);
					runningAnimations[3] = true;
					runningAnimations[5] = true;
				}
				break;
			case 5:
				break;
			case 6:
				break;
			case 7:
				break;
			case 8:
				break;
			case 9:
				break;
			case 10:
				if(runningAnimations[9])
				{
					StartCoroutine(happyDanceCheck());
				}
				break;
			default:
				break;
		}
	}
	private IEnumerator happyDanceCheck()
	{
		runningAnimations[9] = false;
		yield return StartCoroutine(happyDance());
		yield return new WaitForSeconds(Random.Range(5,15));
		runningAnimations[9] = true;
		
	}
	public IEnumerator happyDance()
	{
		anima.ChangeAnimation(3);
//		anima.loop = false;
		
		yield return new WaitForSeconds(1.5f);
		
//		anima.loop = true;
		anima.ChangeAnimation(animationIndex);
	}
}
