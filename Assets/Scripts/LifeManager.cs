using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public int startingLives;
	public string mainMenu;
	public float fadeSpeed = 0f;
	public float fadeTime = .5f;

	public GameObject gameOverScreen;
	//public AudioSource lifeLostSound;

	private int lifeCounter;
	private bool activated = false;
	private Text theText;


	// Use this for initialization
	void Start () {

		//lifeLostSound = GetComponent<AudioSource> ();

		theText = GetComponent<Text> ();
		lifeCounter = startingLives;
	}

	void Update(){
		if (lifeCounter < 1) 
		{
			gameOverScreen.SetActive (true);

			Time.timeScale = 0;
		}

		theText.text = "x " + lifeCounter;
	}

	public void takeLife()
	{
		if (lifeCounter > 0 )
		{
			lifeCounter--;

			//lifeLostSound.Play ();
		}
	}
		


}
