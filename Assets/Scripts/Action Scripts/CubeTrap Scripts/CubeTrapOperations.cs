using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrapOperations : MonoBehaviour {
	public GameObject FoldingCubeTrap;
	public GameObject player;
	private Fade [] myScripts;

	public GameObject backgroundLayer;
	Transform transformPlayer;

	// Use this for initialization
	void Start () 
	{
		transformPlayer = player.transform;
	}

	//FUNCTIONS
	public void activateTrap()
	{
		
		FoldingCubeTrap.transform.Find("Cube Trap").transform.Find("Vertex1").GetComponent<FoldMe> ().enabled = true;
		FoldingCubeTrap.transform.Find("Cube Trap").transform.Find ("Vertex2").GetComponent<FoldMe> ().enabled = true;
		FoldingCubeTrap.transform.Find("Cube Trap").transform.Find ("Vertex2").transform.Find("Vertex3").GetComponent<FoldMe> ().enabled = true;

		//Start TracePath script and change background motion at the same time
		FoldingCubeTrap.GetComponent<TracePath>().enabled = true;

		//Change background speed once cube starts folding
		backgroundLayer.GetComponent<movingBackgroundVertical>().speed = 1;

		//Start Coroutine
		newDelay ();
	}

	public void deactivateTrap()
	{
		myScripts = FoldingCubeTrap.GetComponentsInChildren<Fade>();
		foreach (Fade aScript in myScripts) 
		{
			aScript.enabled = true;
		}
		revertBackgroundChanges ();
		delayDestruction ();
	}

	private void triggerBackgroundScriptChanges()
	{
		var script = backgroundLayer.GetComponent<LightSpeedBackground> ();
		script.enabled = true;
		//script.rotate90Degrees ();
	}

	private void revertBackgroundChanges()
	{
		var script = backgroundLayer.GetComponent<LightSpeedBackground> ();
		script.rotateReverse ();
	}

	//TRIGGERS
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Trigger Detected");
		if (other.gameObject.tag == "Player") 
		{
			activateTrap ();
		}
	}

	//COROUTINES
	private void newDelay () {StartCoroutine ("newDelayCo");}
	private void delayDestruction(){StartCoroutine ("delayDestructionCo");}

	private IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (15);
		FoldingCubeTrap.transform.GetComponent<IntervalRotate> ().enabled = true;

		triggerBackgroundScriptChanges ();
	}
		
	private IEnumerator delayDestructionCo()
	{
		yield return new WaitForSecondsRealtime (15);
		Destroy (FoldingCubeTrap);
	}
}
