using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public GameObject currentCheckpoint;
	public GameObject deathParticle;
	public GameObject respawnParticle;

	public float respawnDelay;

	private PlayerControllerScript player;
	private bool notDead = true;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerControllerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RespawnPlayer (){
		StartCoroutine ("RespawnPlayerCo");
		
	}

	public IEnumerator RespawnPlayerCo(){
		Debug.Log ("Player Respawn");

		if (notDead) {

			Instantiate (deathParticle, player.transform.position, player.transform.rotation);
			notDead = false;

			player.enabled = false;
			player.GetComponent<Renderer> ().enabled = false;

			yield return new WaitForSeconds (respawnDelay);

			player.transform.position = currentCheckpoint.transform.position;
			player.enabled = enabled;
			player.GetComponent<Renderer> ().enabled = enabled;

			Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
			notDead = true;
		}
	}
}
