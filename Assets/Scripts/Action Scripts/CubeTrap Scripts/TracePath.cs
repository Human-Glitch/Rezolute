using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePath : MonoBehaviour {

	public GameObject [] checkpoint;
	public GameObject IntervalRotateCube;
	public int hashTime;
	public int nextHashTime;
	public int hashDelay;

	//Make private later
	public int counter = 0;

	//private int maxSize;

 
	// Use this for initialization
	void Start () 
	{
		//maxSize = checkpoint.Length;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//
		if (counter >= 0) {
			if (counter > 0 && counter < 2) {
				hashTime = nextHashTime;
				IntervalRotateCube.GetComponent<IntervalRotate> ().enabled = true;
			}

			if (counter == 2) 
			{
				Debug.Log ("Cube destroyed at count: " + counter.ToString ());
				//movingBackgroundVertical.speed = .1f;
				newDelay ();
				
			}

			if (counter >= 0 && counter < 2) 
			{
				iTween.MoveUpdate (gameObject, 
					iTween.Hash ("x", (checkpoint [counter].transform.position.x), 
						"y", checkpoint [counter].transform.position.y, 
						"time", hashTime,
						"delay", hashDelay, 
						"onupdate", " myUpdateFunction"//, 
						//"looptype", iTween.LoopType.loop
					)
				);	
			}
		}
	}//end update
		
	//FUNCTIONS
	public void incrementCounter() {counter += 1;}

	void destroy(){
		Destroy (gameObject);
	}

	//COROUTINES
	public void newDelay () {StartCoroutine ("newDelayCo");}

	public IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (3);
		IntervalRotateCube.GetComponentInChildren<CubeTrapOperations>().deactivateTrap ();
	}

}
