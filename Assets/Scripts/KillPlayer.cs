using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	private LevelManager levelManager;//does not work with prefabs so keep private
	private LifeManager lifeManager;
	public AudioSource dyingSound;

	// Use this for initialization
	void Start () {
		dyingSound = GetComponent<AudioSource> ();

		lifeManager = FindObjectOfType<LifeManager> ();
		levelManager = FindObjectOfType<LevelManager> ();// finds an object with level manager attacthed

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			lifeManager.takeLife ();
			dyingSound.Play ();
			levelManager.RespawnPlayer ();
		}
	}
}
