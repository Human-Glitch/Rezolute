using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	public LevelManager levelManager;


	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();// finds an object with level manager attacthed

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			levelManager.RespawnPlayer ();
		}
	}
}
