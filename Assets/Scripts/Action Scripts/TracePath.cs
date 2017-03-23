using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePath : MonoBehaviour {

	public GameObject [] checkpoint;
	public GameObject IntervalRotateCube;
	public int hashTime;
	public int hashDelay;
	private int maxSize;
	public static int counter = 0;
 
	// Use this for initialization
	void Start () 
	{
		maxSize = checkpoint.Length;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (counter >= 0 && counter < maxSize) 
		{
			//Debug.Log ("TracePath MaxSize of Counter " + maxSize.ToString());
			//Debug.Log ("TracePath Checkpoint Counter " + counter.ToString());

			if (counter > 0 && counter < 2) 
			{
				hashTime = 30;
				IntervalRotateCube.GetComponent<IntervalRotate>().enabled = true;

			}

			iTween.MoveUpdate (gameObject, 
													iTween.Hash ("x", (checkpoint[counter].transform.position.x), 
																			"y", checkpoint[counter].transform.position.y, 
																			"time", hashTime,
																			"delay", hashDelay, 
																			"onupdate", " myUpdateFunction"//, 
																			//"looptype", iTween.LoopType.loop
																			)
													);	
		}
	}
}
