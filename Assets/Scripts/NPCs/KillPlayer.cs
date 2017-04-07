using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	private LevelManager levelManager;//does not work with prefabs so keep private
	private GameObject lvl2Scanner;

	private AudioSource dyingSound;

	// Use this for initialization
	void Start () 
	{
		detectScannerObj ();

		dyingSound = GetComponentInParent<AudioSource> ();
		levelManager = FindObjectOfType<LevelManager> ();// finds an object with level manager attacthed
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			var isMoving = other.GetComponent<PlayerControllerScript> ().detectPlayerMovement ();

			//Die if moving through Blue Scanner
			if (lvl2Scanner != null && lvl2Scanner.tag == "Blue Scanner" && isMoving == true) 
			{
				Debug.Log ("Blue Scanner detected");
				dyingSound.Play ();
				levelManager.RespawnPlayer ();
			}

			//Die if not moving through Red Scanner
			if (lvl2Scanner != null && lvl2Scanner.tag == "Red Scanner" && isMoving == false) {
				Debug.Log ("Red Scanner Detected");
				dyingSound.Play ();
				levelManager.RespawnPlayer ();
			} else if (lvl2Scanner == null) {
				dyingSound.Play ();
				levelManager.RespawnPlayer ();
			}
		}
	}

	private void detectScannerObj()
	{
		if (GameObject.FindWithTag("Blue Scanner") != null)
			lvl2Scanner = GameObject.FindWithTag ("Blue Scanner");
		else
			Debug.Log ("Blue Scanner Not Found");

		if (GameObject.FindWithTag("Red Scanner") != null)
			lvl2Scanner = GameObject.FindWithTag ("Red Scanner");
		else 
			Debug.Log ("Red Scanner Not Found");
	}
}
