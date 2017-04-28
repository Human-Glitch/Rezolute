using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementCheckpoint : MonoBehaviour 
{
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
				Debug.Log ("Annoyed and Concerned");
				isWaiting = true;
				Debug.Log ("Counter Incremented");
				GameObject.Find("Folding Cube Trap").GetComponent<TracePath>().incrementCounter ();
			}
		} else if (other.gameObject.tag == "MainCamera")
		{
			if (isWaiting == false) 
			{
				Debug.Log ("Annoyed and Concerned");
				isWaiting = true;
				Debug.Log ("Counter Incremented");
				newDelayIncrementCo (other);
			}
		}
		else{ 
			if(isWaiting == true)
			{
				Debug.Log ("Object waiting for next increment");
				newDelay ();
			}
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
		isWaiting = false;
	}

	public void newDelayIncrementCo (Collider2D other)
	{
		StartCoroutine (newDelayIncrement(other));
	}

	public IEnumerator newDelayIncrement(Collider2D other)
	{
		yield return new WaitForSecondsRealtime (5);
		other.transform.parent.GetComponent<TracePath>().incrementCounter ();
		isWaiting = false;
	}
}
