//http://gamedev.stackexchange.com/questions/130635/how-to-rotate-within-a-fixed-interval-in-unity

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampedRotation : MonoBehaviour 
{
	// Specify how many degrees you want to rotate from the initial orientation.
	public Vector3 amplitude = new Vector3(30, 0, 0);

	// Specify how many seconds a complete loop should take.
	public float period = 6f;

	// Cache initial orientation so we can rotate relative to that pose.
	Quaternion _initialOrientation;

	// Perform caching on Start.
	void Start() 
	{
		_initialOrientation = transform.localRotation;
	}

	void Update ()
	{
		// Calculate how far along we should be in the wave.
		float phase = Mathf.PI * 2f * Time.time / period;

		// Calculate the angle at this part of the wave.
		Vector3 angle = amplitude * Mathf.Cos (phase);

		// Rotate by angle relative to our initial orientation.
		transform.localRotation = _initialOrientation * Quaternion.Euler (angle);
	}
}