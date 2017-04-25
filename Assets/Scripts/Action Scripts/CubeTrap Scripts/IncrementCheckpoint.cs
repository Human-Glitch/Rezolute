using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementCheckpoint : MonoBehaviour {

	private bool isWaiting;

	// Use this for initialization
	void Start () 
	{
		isWaiting = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Trigger Detected");
		if (other.gameObject.tag != "Player" && 
				other.gameObject.tag == "CubeTrap") 
		{
			if (isWaiting == false) 
			{
				//Debug.Log ("Annoyed and Concerned");
				isWaiting = true;
				Debug.Log ("Counter Incremented");
				GameObject.Find("Folding Cube Trap").GetComponent<TracePath>().incrementCounter ();
			}
		} else 
			if(isWaiting == true)
			{
				newDelay ();
			}
	}

	//COROUTINES
	public void newDelay ()
	{
		StartCoroutine ("newDelayCo");
	}

	public IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (5);
		//isWaiting = false;
	}
}
