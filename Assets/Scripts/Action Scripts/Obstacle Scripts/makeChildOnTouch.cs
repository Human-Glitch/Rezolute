//This script is used to add the moving platforms speed to the player's speed
//by making the platform the player's parent on touch and undoing that when the player isn't touching

//This script belongs on a moving object that isnt the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeChildOnTouch : MonoBehaviour 
{
	private void OnCollisionEnter2D (Collision2D player)
	{
		Debug.Log ("New parent!");
		if (player.gameObject.tag == "Player") 
			player.transform.SetParent (gameObject.transform, true);
	}

	private void OnCollisionExit2D(Collision2D player)
	{
		Debug.Log("Left the Nest!");
		if(player.gameObject.tag == "Player")
			player.transform.parent = null;
	}
}