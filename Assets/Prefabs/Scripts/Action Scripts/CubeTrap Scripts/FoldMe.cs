using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldMe : MonoBehaviour 
{
	//private bool hasStopped;
	private float rotation = 0; 
	private float rotationAmt = 0;

	public float targetRotation = 90f;
	public float velocity = 1f;

	void Start()
	{
		//hasStopped = false;
	}

	void Update ()
	{
		rotationAmt = velocity * Time.deltaTime;

		if (Mathf.RoundToInt(Mathf.Abs(rotation)) < targetRotation) 
		{
			//Debug.Log ("Entered Normal");
			transform.Rotate (0, 0, rotationAmt);
			rotation += rotationAmt;
		} 
	}
}
