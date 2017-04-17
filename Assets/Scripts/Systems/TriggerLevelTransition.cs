using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLevelTransition : MonoBehaviour 
{
	public MainMenu mainMenu;
	public GameObject whiteScreen;

	void OnTriggerEnter2D( Collider2D other)
	{
		if (other.tag == "Player")
		{
			levelTransitionCo ();
		}
	}

	private void levelTransitionCo ()
	{
		StartCoroutine ("levelTransition");
	}

	private IEnumerator levelTransition()
	{
		Time.timeScale = .1f;
		yield return new WaitForSecondsRealtime (1.2f);
		whiteScreen.SetActive (true);
		yield return new WaitForSecondsRealtime (1.8f);
		mainMenu.BeginNextLevel ();
	}
}
