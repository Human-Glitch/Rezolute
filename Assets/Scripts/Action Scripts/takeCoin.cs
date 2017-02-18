using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class takeCoin : MonoBehaviour {
	public Text coinCounter;
	public GameObject gameOverScreen;
	public GameObject allCoins;

	private AudioSource coinChing;

	private int score = 0;
	private int totalCoins = 17;

	// Use this for initialization
	void Start () {
		coinChing = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (score == totalCoins ) 
		{
			allCoins.SetActive (true);
			gameOverScreen.SetActive (true);

			Time.timeScale = 0;
		}

	}

	public void IncrementCounter(){
		coinCounter.text = string.Format("Coin: {0}", ++score);
		coinChing.Play ();
	}
		
}
