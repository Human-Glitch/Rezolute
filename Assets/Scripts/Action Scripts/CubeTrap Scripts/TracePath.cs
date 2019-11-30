//This scripts moves the Cubetrap along a traced path  and performs specific actions at certain checkpoints

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePath : MonoBehaviour
{
	public GameObject [] checkpoint;
	public GameObject IntervalRotateCube;
	public int hashTime;
	public int nextHashTime;
	public int hashDelay;

	private int counter = 0;
	//==============================================================
	
	// Update is called once per frame
	void Update () 
	{
		//What actions should be performed every frame at each checkpoint array position
		if (counter >= 0) 
		{
			//change the time it takes to get to the next checkpoint
			if (counter == 1) {
				hashTime = nextHashTime;

				if(gameObject.tag == "CubeTrapContainer")
					IntervalRotateCube.GetComponent<IntervalRotate> ().enabled = true;
			}

			//Deactivate trap after a delay
			if (counter == 2 && gameObject.tag == "CubeTrapContainer") 
			{
				newDelay ();
				
			}

			//Move while not at the destination
			if (counter >= 0 && counter < checkpoint.Length) 
			{
				iTween.MoveUpdate (gameObject, 
					iTween.Hash ("x", (checkpoint [counter].transform.position.x), 
						"y", checkpoint [counter].transform.position.y, 
						"time", hashTime,
						"delay", hashDelay, 
						"onupdate", "myUpdateFunction"
					)
				);	
			}
		}
	}//end update
		
	//FUNCTIONS
	//==============================================================
	public void incrementCounter() {counter += 1;}

	void destroy(){
		Destroy (gameObject);
	}

	//COROUTINES
	//==============================================================
	public void newDelay () {StartCoroutine ("newDelayCo");}

	public IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (3);

		if (gameObject.tag == "CubeTrap" || gameObject.tag == "CubeTrapContainer") 
		{
			Debug.Log ("Cube destroyed at count: " + counter.ToString ());
			IntervalRotateCube.GetComponentInChildren<CubeTrapOperations> ().deactivateTrap ();
		}
	}

}
