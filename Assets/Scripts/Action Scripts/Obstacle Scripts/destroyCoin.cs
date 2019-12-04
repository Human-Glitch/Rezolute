//This script destroys a coin on touch and creates a green particle effect splash

using UnityEngine;

public class DestroyCoin : MonoBehaviour 
{
	public GameObject particleEffect;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag != "Player") return;

		GameObject.FindGameObjectWithTag("UIFragments").GetComponent<PlayerStatManager>().IncreaseDataFragments();
		var particleSplash = (Instantiate (particleEffect, gameObject.transform.position, 
				                    gameObject.transform.rotation) as GameObject);

		particleSplash.transform.localScale = transform.localScale / 2.5f;
        Destroy(particleSplash, 1f);
		Destroy (gameObject);
	}
}
