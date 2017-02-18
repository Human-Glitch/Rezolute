using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string startLevel;

	public void PlayAgain()
	{
		Debug.Log ("New Game started");
		Application.LoadLevel (startLevel);

	}

	public void ExitGame()
	{
		Debug.Log ("Game exited");
		Application.Quit ();
	}
}
