/// <summary>
/// This script determines the behavior of the spawned scanner 
/// based off the settings initialized from <CreateScanner>.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TranslationEnums;

public class ScannerPattern : MonoBehaviour 
{
	private bool isPatrolPattern;
	private bool isActivePattern;

	private bool reachedTarget = false;

	private float hashTime;
	private float hashDelay;
	private Vector3 spawnPoint;

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
			CompleteITweenCo ();
		}

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
			);
			TimedDeathCo ();
		}
	}

    #region FUNCTIONS
    public void Initialize(Vector3 spawnPoint, bool isPatrolPattern, bool isActivePattern, TranslationPattern selectedTranslationPattern, 
        float hashTime, float hashDelay)
	{
		this.spawnPoint = spawnPoint;
		this.isPatrolPattern = isPatrolPattern;
		this.isActivePattern = isActivePattern;
		this.hashTime = hashTime;
		this.hashDelay = hashDelay;

		//initialize translation variables in Interval translate
		gameObject.GetComponent<IntervalTranslate>().InitializeScannerTranslationPattern (selectedTranslationPattern);
	}

	private void stopITween(){ reachedTarget = true; }
    #endregion FUNCTIONS

    #region COROUTINES
    private void TimedDeathCo ()
	{
		StartCoroutine ("timedDeath");
	}

	private IEnumerator TimedDeath()
	{
		Destroy (this.gameObject, 5);
		yield return new WaitForSecondsRealtime(4f);
		Debug.Log ("Destroyed scanner");
		gameObject.GetComponent<Fade> ().enabled = true;
	}

	private void CompleteITweenCo ()
	{
		StartCoroutine ("completeITween");
	}

	private IEnumerator CompleteITween()
	{
		yield return new WaitForSecondsRealtime(hashTime - .5f);
		stopITween ();
	}
    #endregion COROUTINES
}
