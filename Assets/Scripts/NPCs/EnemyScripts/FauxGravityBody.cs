//This script is given to the object you want to have fake gravity
//This works by telling the object that it's local position is the world position as well

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour 
{
	public FauxGravityAttractor attractor;
	private Transform myTransform;
	//==============================================================

	// INITIALIZATION
	void Start () 
	{
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		GetComponent<Rigidbody2D>().gravityScale = 0;
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(attractor != null)
			attractor.Attract (myTransform);
	}
}
