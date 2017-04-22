//This script creates an internval translation without rounding errors 
//of my the Interval Translation Script. This script was modified from this website:

//https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
//by Robert Utter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTranslation: MonoBehaviour 
{
	private bool hasStopped;
	private float originalTarget = 0;

	public float translationDelay = 0f;

	//Test Variables
	[Header("Test Variables")]
	public float lerpTime = 1f;
	public float currentLerpTime;

	public float moveDistance = 10f;

	public Vector3 startPos;
	public Vector3 endPos;


	void Start()
	{
		hasStopped = false;
		originalTarget = moveDistance;

		//Test
		startPos = transform.position;
		endPos = transform.position + transform.up 
			* moveDistance;
	}

	void Update ()
	{
		//Test

//		if (Input.GetKeyDown(KeyCode.Space)) {
//			currentLerpTime = 0f;
//		}

		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}
		//lerp!
		var percent = currentLerpTime / lerpTime;
		transform.position = Vector3.Lerp(startPos, endPos, percent
		);

		if (currentLerpTime == lerpTime && !hasStopped)
			delayTranslation2 ();
	}

	private void delayTranslation2()
	{
		StartCoroutine ("delayTranslationCo2");
	}

	private IEnumerator delayTranslationCo2()
	{
		hasStopped = true;
		yield return new WaitForSeconds (translationDelay);

		reverseTranslationValues ();

		currentLerpTime = 0;
		hasStopped = false;
	}

	private void reverseTranslationValues()
	{
		if(startPos == endPos)
		{
			var temp = startPos;
			startPos = endPos;
			endPos = startPos;
		}
			
		if (moveDistance == originalTarget) 
		{
			moveDistance = -originalTarget;
			startPos = transform.position;
			endPos = transform.position + transform.up 
				* moveDistance;
		} else {
			moveDistance = originalTarget;
			startPos = transform.position;
			endPos = transform.position + transform.up 
				* moveDistance;
		}
	}

}//end class
