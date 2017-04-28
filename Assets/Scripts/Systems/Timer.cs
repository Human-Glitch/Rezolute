using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	private GameObject timerCount;
	public GameObject gameOverScreen;
	public GameObject timesUp;

	public float timer = 60f;
	private Text theText;

	// Use this for initialization
	void Start () {
		theText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (timer >= 0) {
			timer = timer - Time.deltaTime;
			theText.text = " " + Mathf.Round ((timer * 100f) / 100f) + " sec";
		} else {
			timesUp.SetActive (true);
			gameOverScreen.SetActive (true);
			Time.timeScale = 0f;
		}
			
	}
}
