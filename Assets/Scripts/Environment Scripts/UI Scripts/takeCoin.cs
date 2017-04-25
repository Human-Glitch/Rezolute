using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class takeCoin : MonoBehaviour {
	public Text coinCounter;
	public GameObject gameOverScreen;
	//public GameObject allCoins; //not in this game
	public bool coinsExist;

	private AudioSource beepBloop;
	private int score;
	public int totalCoins;

	// Use this for initialization
	void Start () 
	{
		score = 0;

//		if (coinsExist == true) {
//			totalCoins = GameObject.Find ("Coins").transform.childCount;
			beepBloop = gameObject.GetComponent<AudioSource> ();
//		} else{ totalCoins = 100; }
	}
	
	// Update is called once per frame
	void Update () {
//		if (score == totalCoins ) 
//		{
//			allCoins.SetActive (true);
//			gameOverScreen.SetActive (true);
//
//			Time.timeScale = 0;
//		}

	}

	public void IncrementCounter()
	{
		coinCounter.text = string.Format("Data Fragments {0}", ++score);

		if (score % 10 == 0)
			gameObject.GetComponentInChildren<LifeManager> ().gainLife ();

		beepBloop.Play ();
	}
		
}
