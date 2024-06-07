using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class btnFX : MonoBehaviour {

	public AudioSource myFx;
	public AudioClip hoverFx;
	public AudioClip clickFx;


	public void HoverSound()   
	{
		myFx.PlayOneShot (hoverFx);
	}
	public void ClickSound()
	{
		myFx.PlayOneShot (clickFx);
	}

	public void loadLevel()
	{
		SceneManager.LoadScene ("Scene 2");
	}

}
