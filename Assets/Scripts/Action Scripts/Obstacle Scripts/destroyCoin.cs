//This script destroys a coin on touch and creates a green particle effect splash

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class destroyCoin : MonoBehaviour 
{
	public takeCoin counter; //needs object in order access the script so this is workaround
	public GameObject particleEffect;
	//==============================================================

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			counter.IncrementCounter ();//needs script, but cant call with only public functions
			var particleSplash = (Instantiate (particleEffect, gameObject.transform.position, 
				                     gameObject.transform.rotation) as GameObject);
			particleSplash.transform.localScale = transform.localScale / 2.5f;
			Destroy (gameObject);
		}
	}
}
