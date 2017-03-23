using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpeedBackground : MonoBehaviour {
	public int targetRotation;
	public float targetSpeed;
	public int targetTime;

	// Use this for initialization
	void Start () 
	{
		movingBackgroundVertical.speed = targetSpeed;
		newDelay ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		iTween.RotateTo( gameObject, iTween.Hash(
			"rotation", new Vector3 (0, 0, targetRotation),
			"time"    , targetTime,
			"onUpdate", "UpdateSize",
			"easeType", iTween.EaseType.easeOutSine
		));
	}

	public void rotate90Degrees()
	{
		targetSpeed = 3f;
		targetRotation = -90;
	}

	void newDelay ()
	{
		StartCoroutine ("newDelayCo");
	}

	IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (5);
		gameObject.GetComponent<LightSpeedBackground>().enabled = false;

	}
}
