using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeManager : MonoBehaviour {

	public int startingLives;
	public float maxLives;

	public GameObject gameOverScreen;
	public GameObject [] outOfLives;
	//public AudioSource lifeLostSound;

	private int lifeCounter;
	private Text theText;


	// Use this for initialization
	void Start () {

		//lifeLostSound = GetComponent<AudioSource> ();

		theText = GetComponent<Text> ();
		lifeCounter = startingLives;
	}

	void Update()
	{
		if (lifeCounter < 1 ) 
		{
			gameOverScreen.SetActive (true);
			gameObject.GetComponentInParent<PauseMenu> ().setIsGameOver (true);

			foreach(var text in outOfLives)
				text.SetActive (true);

			Time.timeScale = 0f;
		}

		theText.text = (lifeCounter * 10).ToString () + " %";
	}

	public void takeLife()
	{
		if (lifeCounter > 0 )
		{
			lifeCounter--;

			//lifeLostSound.Play ();
		}
	}

	public void GainLife()
	{
		if (lifeCounter <= maxLives ) //max life
		{
			Debug.Log ("Life Gained");
			lifeCounter++;
		}
	}
}
