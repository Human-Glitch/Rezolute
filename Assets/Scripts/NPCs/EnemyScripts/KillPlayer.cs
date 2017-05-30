/// <summary>
/// This script controls all the ways the player can die
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KillPlayer : MonoBehaviour 
{
	private LevelManager levelManager;//does not work with prefabs so keep private
	private AudioSource dyingSound;
	//=======================================================

	//INITIALIZATION
	void Start () 
	{
		dyingSound = GetComponentInParent<AudioSource> ();
		levelManager = FindObjectOfType<LevelManager> ();// finds an object with level manager attacthed
	}

	//TRIGGER
	//=======================================================
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.tag == "Player") 
		{
			methods2Kill (other);
		}
	}

	//FUNCTIONS
	//=======================================================
	private void methods2Kill(Collider2D other)
	{
		
		if (Application.loadedLevelName == "Level 2" 
			&& gameObject.transform.tag != "Enemy"
			&& gameObject.transform.parent.tag != "Boss" 
			&& gameObject.transform.tag != "Fall Detector")
				scannerDeath (other);
		
		else
		{
			Debug.Log ("Normal Death");
			dyingSound.Play ();
			levelManager.RespawnPlayer ();
		}
	}

	void scannerDeath(Collider2D other)
	{
		var otherIsMoving = other.GetComponent<PlayerControllerScript> ().detectPlayerMovement (); //Store player movement data for later checks
		var otherIsGrounded = other.GetComponent<PlayerControllerScript> ().detectIfGrounded ();

		if (this.gameObject.transform.parent.tag == "Blue Scanner" && otherIsMoving == true) 
		{
			Debug.Log ("Blue Death");
			dyingSound = GameObject.FindGameObjectWithTag ("Audio").GetComponent<AudioSource>();
			dyingSound.Play ();
			levelManager.RespawnPlayer ();
		}

		//Die if not moving through Red Scanner
		if (gameObject.transform.parent.tag == "Red Scanner" && otherIsGrounded == true) 
		{
			Debug.Log ("Red Death");
			dyingSound = GameObject.FindGameObjectWithTag ("Audio").GetComponent<AudioSource>();
			dyingSound.Play ();
			levelManager.RespawnPlayer ();

			//gameObject.transform.parent.gameObject.SetActive(false); // hide Scanner after player death
		}

		//	Debug.Log(this.gameObject.transform.parent.tag);
		//Debug.Log (otherIsMoving);
	}
}
