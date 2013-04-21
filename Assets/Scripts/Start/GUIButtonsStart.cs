using UnityEngine;
using System.Collections;

public class GUIButtonsStart : MonoBehaviour {
	
	public bool OnCredits = false;
	
	public Renderer fader;
	
	public GUITexture AGLogo, StartBtn, CreditsBtn;
	public Renderer CreditsScreen;
	
	public AudioSource startBtnClick;
	public AudioSource creditsBtnClick;
	public AudioSource MainTheme;
	
	public GUISkin skin;
	public Rect r, r2;
	
	public float ryPercStart,ryPercCredits, rxPercStart, rxPercCredits;
	
	void Start()
	{
		r.x = Screen.width * rxPercStart - r.width/2;
		r.y = Screen.height * ryPercStart - r.height/2;
		
		r2.x = Screen.width * rxPercCredits - r2.width/2;
		r2.y = Screen.height * ryPercCredits - r2.height/2;
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		if(!OnCredits)
		{
			if(GUI.Button(r,""))
			{
				//PlayAnimation
				startBtnClick.Play();
				StartCoroutine(startFadeAndLoad());
				AGLogo.enabled = false;
				StartBtn.enabled = false;
				CreditsBtn.enabled = false;
			}
			else if(GUI.Button(r2,""))
			{
				OnCredits = true;
				AGLogo.enabled = false;
				StartBtn.enabled = false;
				CreditsBtn.enabled = false;
				CreditsScreen.gameObject.SetActive(true);
				creditsBtnClick.Play();
			}
		}
		else
		{
			if(GUI.Button(r2,""))
			{
				OnCredits = false;
				AGLogo.enabled = true;
				StartBtn.enabled = true;
				CreditsBtn.enabled = true;
				CreditsScreen.gameObject.SetActive(false);
				creditsBtnClick.Play();
			}
		}
	}
	private IEnumerator startFadeAndLoad()
	{
		//fade music
		StartCoroutine(fadeMusic(MainTheme,-1));
		//fade screen
		//wait for them to be done
		yield return StartCoroutine(TextureFade(fader.material,1));
		//loadscreen
		Application.LoadLevel("IntroMovieLoader");
	}
	private IEnumerator fadeMusic(AudioSource a, float rate)
	{
		while(a.volume >= 0 && a.volume <= 1)
		{
			a.volume += rate * Time.deltaTime;
			yield return null;
		}
		if(rate > 0)
		{
			a.volume = 1;
		}
		else
		{
			a.volume = 0;
		}
	}
	private IEnumerator TextureFade(Material mat, float rate)
	{
		float r,g,b,a;
		r = g = b = 0;
		a = 0;
		mat.color = new Color(r,g,b,a);
		while(a < 1)
		{
			a = Mathf.MoveTowards(a, 1, rate * Time.deltaTime);
			mat.color = new Color(r,g,b,a);
			yield return null;
		}
	}
}
