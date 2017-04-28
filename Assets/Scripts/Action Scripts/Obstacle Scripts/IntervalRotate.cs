using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalRotate : MonoBehaviour 
{
	private bool hasStopped;
	private float rotation = 0; 
	private float rotationAmt = 0;

	public float targetRotation = 90f;
	public float rotationDelay = 0f;
	public float rotateSpeed = 1f;
	private int rotationCount;

	void Start()
	{
		rotationCount = 0;
		hasStopped = false;
	}

	void Update ()
	{
		rotationAmt = rotateSpeed * Time.deltaTime;

		if ((Mathf.Abs(rotation)) < targetRotation) 
		{
			//Debug.Log ("Entered Normal");
			transform.Rotate(0, 0, rotationAmt, Space.Self); 
			rotation += rotationAmt;
		} 
		else
		{
			//Debug.Log ("Else reached");

			if(hasStopped == false)
			{
				rotationCount++;
				delayRotation ();
			}
		}
	}

	public void delayRotation()
	{
		StartCoroutine ("delayRotationCo");
	}

	public IEnumerator delayRotationCo(){

		hasStopped = true;

		rotation = 0;
		rotationAmt = 0;

		yield return new WaitForSecondsRealtime (rotationDelay);
		//Debug.Log ("Block Stopped");

		hasStopped = false;
	}

	public int getRotationCount(){ return rotationCount; }

}
