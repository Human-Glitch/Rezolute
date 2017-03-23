using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal2Tracer : MonoBehaviour {
	public GameObject rotationIntervalCube;
	public GameObject player;
	public GameObject backgroundLayer;
	Transform transformPlayer;

	// Use this for initialization
	void Start () {
		transformPlayer = player.transform;

		movingBackgroundVertical.speed = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Trigger Detected");
		if (other.gameObject.tag == "Player") 
		{
			activateSignal ();
		}
	}

	public void activateSignal()
	{
		
		rotationIntervalCube.transform.Find("Cube Trap").transform.Find("Vertex1").GetComponent<FoldMe> ().enabled = true;
		rotationIntervalCube.transform.Find("Cube Trap").transform.Find ("Vertex2").GetComponent<FoldMe> ().enabled = true;
		rotationIntervalCube.transform.Find("Cube Trap").transform.Find ("Vertex2").transform.Find("Vertex3").GetComponent<FoldMe> ().enabled = true;
		rotationIntervalCube.GetComponent<TracePath>().enabled = true;

		movingBackgroundVertical.speed = 1;

		newDelay ();
	}

	public void newDelay ()
	{
		StartCoroutine ("newDelayCo");
	}

	//public void setNewParent(Transform transformPlayer){ cubeTrap.transform.SetParent (transformPlayer, true);}

	public IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (15);
		rotationIntervalCube.transform.GetComponent<IntervalRotate> ().enabled = true;

		var script = backgroundLayer.GetComponent<LightSpeedBackground> ();
		script.enabled = true;
		script.rotate90Degrees ();
	}
}
