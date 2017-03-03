using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testFold : MonoBehaviour {
	public GameObject vertex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newVertex = new Vector3 (0,0, 30);
		newVertex.z += 5;
		newVertex = transform.InverseTransformPoint (newVertex);
	}
}
