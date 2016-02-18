using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class takeCoin : MonoBehaviour {
	public Text coinCounter;
	private AudioSource coinChing;

	private int score = 0;

	// Use this for initialization
	void Start () {
		coinChing = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void IncrementCounter(){
		coinCounter.text = string.Format("Coin: {0}", ++score);
		coinChing.Play ();
	}
		
}
