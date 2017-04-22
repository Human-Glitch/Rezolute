using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordCloudFade : MonoBehaviour 
{
	public float fadeSpeed = 0f;
	public float fadeLength;

	public float timeActive;

	public bool transparencyGoingUp ;
	public bool transparencyGoingDown;


	private bool startedDelay;
	//private Transform transform;
	private MeshRenderer meshRenderer;

	Color col;
	Color originalCol;

	// Use this for initialization
	void Start () 
	{
		//transform = transform;
		timeActive = 0;
		meshRenderer = GetComponent<MeshRenderer> ();
		col = meshRenderer.material.color;
		originalCol = col;
		
	}

	// Update is called once per frame
	void Update ()
	{
		if (transparencyGoingUp) 
		{
			timeActive += Time.deltaTime;
			col = meshRenderer.material.color;
			col.a = Mathf.Lerp (col.a, 0f, fadeSpeed * timeActive);
			meshRenderer.material.color = col;

			if(timeActive > fadeLength && !startedDelay)
				delayNewDirectionCo ();

		} else if (transparencyGoingDown) 
		{
			timeActive += Time.deltaTime;
			col = meshRenderer.material.color;
			col.a = Mathf.Lerp (originalCol.a, 1f, fadeSpeed * timeActive);
			meshRenderer.material.color = col;

			if(timeActive > fadeLength && !startedDelay)
				delayNewDirectionCo ();
		}
	}

	private void delayNewDirectionCo ()
	{
		StartCoroutine ("delayNewDirection");
	}

	private IEnumerator delayNewDirection()
	{
		startedDelay = true;


		yield return new WaitForSecondsRealtime (1.2f);

		if(transparencyGoingUp == true)
		{
			transparencyGoingUp = false;
			transparencyGoingDown = true;
		}
		else if(transparencyGoingDown == true)
		{
			transparencyGoingUp = true;
			transparencyGoingDown = false;
		}

		timeActive = 0;

		startedDelay = false;
	}
}

