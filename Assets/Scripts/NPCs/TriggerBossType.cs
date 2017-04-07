using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossType : MonoBehaviour 
{
	public bool isPlantedType;
	public bool isActiveType;
	public float hashTime;
	public float hashDelay;

	public GameObject redBoss;
	public GameObject blueBoss;
	public GameObject spawnPoint;

	private GameObject plantedBoss;
	private GameObject activeBoss;

	private bool startUpdate;

	// Use this for initialization
	void Start () 
	{ 
		startUpdate = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (startUpdate == true) {
			//var position = plantedBoss.transform.position.y;
			//plantedBoss.transform.Translate (0, .01f , 0);

			if (isPlantedType == true) {
				iTween.MoveUpdate (plantedBoss, 
					iTween.Hash ("x", (transform.position.x + 20), 
						"y", transform.position.y + 5, 
						"time", hashTime,
						"delay", hashDelay, 
						"onupdate", " myUpdateFunction",
						"oncomplete", "Destroy"//, 
						//"looptype", iTween.LoopType.loop
					)
				);	
			}// end planted type

			if (isActiveType == true) {
				iTween.MoveUpdate (activeBoss, 
					iTween.Hash ("x", (transform.position.x - 40), 
						"y", transform.position.y + 5, 
						"time", hashTime,
						"delay", hashDelay, 
						"onupdate", " myUpdateFunction",
						"oncomplete", "Destroy"//, 
						//"looptype", iTween.LoopType.loop
					)
				);	
			}//end activetype

		}//end start update
		
	}//end update

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			if(isPlantedType == true)
			{
				plantedBoss = Instantiate (redBoss, 
					new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
					transform.rotation) as GameObject;
				startUpdate = true;
			}

			if (isActiveType == true) 
			{
				activeBoss = Instantiate (blueBoss, 
					new Vector3(transform.position.x + 20, transform.position.y + 5, transform.position.z), 
					transform.rotation) as GameObject;
				startUpdate = true;
			}
		}
	}

	void Destroy()
	{
		Debug.Log ("Destroyed");
		if (isPlantedType == true)
			Destroy (plantedBoss);
		if (isActiveType == true)
			Destroy (activeBoss);
	}
}
