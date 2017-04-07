using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillPlayer : MonoBehaviour {

	private LevelManager levelManager;//does not work with prefabs so keep private
	private AudioSource dyingSound;

	// Use this for initialization
	void Start () 
	{
		
		dyingSound = GetComponentInParent<AudioSource> ();
		levelManager = FindObjectOfType<LevelManager> ();// finds an object with level manager attacthed
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "Player") 
		{
			methods2Kill (other);
		}
	}
		
	private void methods2Kill(Collider2D other)
	{
		var otherIsMoving = other.GetComponent<PlayerControllerScript> ().detectPlayerMovement (); //Store player movement data for later checks
		var otherIsGrounded = other.GetComponent<PlayerControllerScript> ().detectIfGrounded ();

		//Die if moving through Blue Scanner
		//var newestScanner = TriggerBossType.returnNewestScannerPrefab();

		Debug.Log(this.gameObject.transform.parent.tag);
		Debug.Log (otherIsMoving);
		if (this.gameObject.transform.parent.tag == "Blue Scanner" && otherIsMoving == true) 
		{
			Debug.Log ("Blue Death");
			dyingSound.Play ();
			levelManager.RespawnPlayer ();

			gameObject.transform.parent.gameObject.SetActive(false); // hide Scanner after player death
		}
			
		//Die if not moving through Red Scanner
		else if (gameObject.transform.parent.tag == "Red Scanner" && otherIsGrounded == true) {
			Debug.Log ("Red Death");
			dyingSound.Play ();
			levelManager.RespawnPlayer ();
			gameObject.transform.parent.gameObject.SetActive(false); // hide Scanner after player death
		} else if(gameObject.transform.parent.tag != "Red Scanner" && gameObject.transform.parent.tag != "Blue Scanner" )
		{
			Debug.Log ("Normal Death");
			dyingSound.Play ();
			levelManager.RespawnPlayer ();
		}
	}
}
