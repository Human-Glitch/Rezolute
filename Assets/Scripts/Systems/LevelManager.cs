using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public GameObject currentCheckpoint;
	public GameObject deathParticle;
	public GameObject respawnParticle;

	public float respawnDelay;

	private LifeManager lifeManager;
	private PlayerControllerScript player;
	private GameObject mainCamera;
	private bool notDead = true;


	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
		player = FindObjectOfType<PlayerControllerScript> ();
		lifeManager = FindObjectOfType<LifeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RespawnPlayer ()
	{
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo()
	{
		Debug.Log ("Player Respawn");

		if (notDead) 
		{
			GameObject deathEffect = Instantiate (deathParticle, player.transform.position, player.transform.rotation);
			notDead = false;
			lifeManager.takeLife ();

			player.enabled = false;
			player.GetComponent<Renderer> ().enabled = false;
	
			yield return new WaitForSeconds (respawnDelay);

			Destroy (deathEffect);

			player.transform.position = currentCheckpoint.transform.position;
			player.enabled = enabled;
			player.GetComponent<Renderer> ().enabled = enabled;
		
			GameObject respawnEffect = Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
			mainCamera.GetComponent<FollowPlayer>().resetCameraPosition ();

			yield return new WaitForSecondsRealtime(respawnDelay);
			notDead = true;
			Destroy (respawnEffect);

		}
	}
}
