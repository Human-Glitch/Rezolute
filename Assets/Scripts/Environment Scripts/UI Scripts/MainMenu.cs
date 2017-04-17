using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;
	public string nextLevel;

	public void PlayAgain()
	{
		Debug.Log ("New Game started");
		Application.LoadLevel (startLevel);

	}

	public void BeginNextLevel()
	{
		Debug.Log ("Next Level Started");
		Application.LoadLevel (nextLevel);

	}

	public void ExitGame()
	{
		Debug.Log ("Game exited");
		Application.Quit ();
	}
}
