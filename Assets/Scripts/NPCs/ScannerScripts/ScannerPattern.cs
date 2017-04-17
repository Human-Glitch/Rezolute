using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerPattern : MonoBehaviour 
{

	private bool isPatrolPattern;
	private bool isActivePattern;
	private bool isRedScanner;
	private bool isBlueScanner;
	private bool goUpFirst;
	private bool goSidewaysFirst;

	private float hashTime;
	private float hashDelay;
	private Vector3 spawnPoint;
	
	// Update is called once per frame
	void Update () {

		if (isPatrolPattern == true) 
		{
			iTween.MoveUpdate (gameObject, 
				iTween.Hash ("x", (spawnPoint.x + 20), 
					"y", spawnPoint.y + 2.5, 
					"time", hashTime,
					"delay", hashDelay, 
					"onupdate", " myUpdateFunction"
					//"oncomplete", "timedDeathCo"
					//"looptype", iTween.LoopType.loop
				)
			);	
		}// end plantedType

		if (isActivePattern == true) 
		{
			iTween.MoveUpdate (gameObject, 
				iTween.Hash ("x", (spawnPoint.x - 40), 
					"y", spawnPoint.y + 6, 
					"time", hashTime,
					"delay", hashDelay, 
					"onupdate", " myUpdateFunction"
					//"looptype", iTween.LoopType.loop
				)
			);	//end iTween
			timedDeathCo ();
		}//end activetype
	}

	public void Initialize(Vector3 spawnPoint, bool isPatrolPattern, bool isActivePattern, float hashTime, float hashDelay)
	{
		this.spawnPoint = spawnPoint;
		this.isPatrolPattern = isPatrolPattern;
		this.isActivePattern = isActivePattern;
		this.hashTime = hashTime;
		this.hashDelay = hashDelay;

		//gameObject.GetComponent<IntervalTranslate> ().initializePattern (this.goUpFirst, this.goSidewaysFirst);


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
