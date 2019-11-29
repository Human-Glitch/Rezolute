/// <summary>
/// This script determines the behavior of the spawned scanner 
/// based off the settings initialized from <CreateScanner>.
/// </summary>

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

	public bool reachedTarget = false;

	private float hashTime;
	private float hashDelay;
	private Vector3 spawnPoint;
	//=======================================================
	
	// Update is called once per frame
	void Update () {

		if (isPatrolPattern == true && !reachedTarget) 
		{
			iTween.MoveUpdate (gameObject, 
				iTween.Hash ("x", (spawnPoint.x + 20), 
					"y", spawnPoint.y + 2.5, 
					"time", hashTime,
					"delay", hashDelay, 
					"onupdate", " myUpdateFunction"
					//"looptype", iTween.LoopType.loop
				)
			);	
			completeITweenCo ();
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

	//Functions
	//=======================================================
	public void Initialize(Vector3 spawnPoint, bool isPatrolPattern, bool isActivePattern, bool goUpFirst,  bool goSidewaysFirst,
		float targetTranslation, float translationSpeed, float translationDelay, float hashTime, float hashDelay)
	{
		this.spawnPoint = spawnPoint;
		this.isPatrolPattern = isPatrolPattern;
		this.isActivePattern = isActivePattern;
		this.hashTime = hashTime;
		this.hashDelay = hashDelay;

		//initialize translation variables in Interval translate
		gameObject.GetComponent<IntervalTranslate> ().InitializeScannerTranslationPattern (goUpFirst, 
			goSidewaysFirst, targetTranslation, translationSpeed, translationDelay);
	}

	private void Destroy()
	{
		Destroy (this.gameObject);
	}

	private void stopITween(){ reachedTarget = true; }

	//COROUTINES
	//=======================================================
	private void timedDeathCo ()
	{
		StartCoroutine ("timedDeath");
	}

	private IEnumerator timedDeath()
	{
		Destroy (this.gameObject, 5);
		yield return new WaitForSecondsRealtime(4f);
		Debug.Log ("Destroyed scanner");
		gameObject.GetComponent<Fade> ().enabled = true;
	}

	private void completeITweenCo ()
	{
		StartCoroutine ("completeITween");
	}

	private IEnumerator completeITween()
	{
		yield return new WaitForSecondsRealtime(hashTime - .5f);
		stopITween ();
	}
}
