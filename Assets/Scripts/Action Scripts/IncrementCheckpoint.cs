using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementCheckpoint : MonoBehaviour {

	bool isWaiting;

	// Use this for initialization
	void Start () 
	{
		isWaiting = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void incrementCounter() {TracePath.counter += 1;}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Trigger Detected");
		if (other.gameObject.tag == "CubeTrap") 
		{
			if (isWaiting == false) 
			{
				//Debug.Log ("Annoyed and Concerned");
				isWaiting = true;
				Debug.Log ("Counter Incremented");
				incrementCounter ();
			}
		} else 
			if(isWaiting == true)
			{
				newDelay ();
				isWaiting = false;
			}
	}

	public void newDelay ()
	{
		StartCoroutine ("newDelayCo");
	}

	public IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (2);
	}
}
