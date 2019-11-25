/// <summary>
/// This creates the effects necessary to destroy the object and kill the boss in level 2
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoss : MonoBehaviour 
{
	private GameObject camera;
	private GameObject boss;
	private Fade fadeScript;
	private ClampedRotation clampedScript;
	private bool doneOnce = false;

	//INITIALIZATION
	void Start()
	{
		camera = GameObject.FindWithTag ("MainCamera");
		boss = GameObject.FindWithTag ("Boss");
		fadeScript = boss.GetComponent<Fade> ();
		clampedScript = boss.GetComponent <ClampedRotation> ();
	}

	//TRIGGER
	//==============================================================
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player" && !doneOnce)
		{
			camera.GetComponent<FollowPlayer> ().setWillTargetX (true);
			camera.GetComponent <FollowPlayer> ().setWillTargetY (true);
			camera.GetComponent<FollowPlayer> ().setForceXScrolling (false);
			camera.GetComponent<FollowPlayer> ().setForceYScrolling (false);

			fadeScript.enabled = true;
			clampedScript.enabled = true;

			doneOnce = true;
		}
	}
}
