using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class takeCoin : MonoBehaviour {
	public Text coinCounter;
	public GameObject gameOverScreen;
	public GameObject allCoins;
	public bool areCoins;

	private AudioSource coinChing;
	private int score;
	public int totalCoins;

	// Use this for initialization
	void Start () 
	{
		score = 0;

		if (areCoins == true) {
			totalCoins = GameObject.Find ("Coins").transform.childCount;
			coinChing = GetComponent<AudioSource> ();
		} else{ totalCoins = 100; }
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

	public void IncrementCounter()
	{
		coinCounter.text = string.Format("Coin: {0}", ++score);
		//coinChing.Play ();
	}
		
}
