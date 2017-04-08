using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerPattern : MonoBehaviour {

	private bool isPlantedType;
	private bool isActiveType;
	private float hashTime;
	private float hashDelay;
	private Vector3 spawnPoint;
	
	// Update is called once per frame
	void Update () {

		if (isPlantedType == true) {
			iTween.MoveUpdate (gameObject, 
				iTween.Hash ("x", (spawnPoint.x + 20), 
					"y", spawnPoint.y + 5, 
					"time", hashTime,
					"delay", hashDelay, 
					"onupdate", " myUpdateFunction"
					//"oncomplete", "timedDeathCo"
					//"looptype", iTween.LoopType.loop
				)
			);	
		}// end plantedType

		if (isActiveType == true) {
			iTween.MoveUpdate (gameObject, 
				iTween.Hash ("x", (spawnPoint.x - 40), 
					"y", spawnPoint.y + 5, 
					"time", hashTime,
					"delay", hashDelay, 
					"onupdate", " myUpdateFunction"
					//"looptype", iTween.LoopType.loop
				)
			);	//end iTween
			timedDeathCo ();
		}//end activetype


	}

	public void Initialize(Vector3 spawnPoint, bool isPlantedType, bool isActiveType, float hashTime, float hashDelay)
	{
		this.spawnPoint = spawnPoint;
		this.isPlantedType = isPlantedType;
		this.isActiveType = isActiveType;
		this.hashTime = hashTime;
		this.hashDelay = hashDelay;
	}

	void Destroy()
	{
		Destroy (this.gameObject);
	}

	private void timedDeathCo ()
	{
		StartCoroutine ("timedDeath");
	}

	private IEnumerator timedDeath()
	{
		yield return new WaitForSecondsRealtime(8);
		Debug.Log ("Destroyed scanner");
		Destroy (this.gameObject);
	}
}
