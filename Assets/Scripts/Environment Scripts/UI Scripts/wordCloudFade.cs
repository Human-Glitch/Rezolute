using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordCloudFade : MonoBehaviour 
{
	public float fadeSpeed;
	public float fadePeriod;
	public float timeActive;

	public bool transparencyGoingUp ;
	public bool transparencyGoingDown;

	private bool startedDelay;

	private GameObject wordCloud;
	private GameObject mainCamera;
	private MeshRenderer meshRenderer;
	private Color col;
	private Color originalCol;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.FindWithTag ("MainCamera");

		if (this.transform.parent != null && this.transform.parent.tag == "PlayerCloud")
			wordCloud = this.transform.parent.gameObject;
			//wordCloud = GameObject.FindWithTag ("PlayerCloud");
		else if(this.transform.parent != null && this.transform.parent.tag == "EnemyCloud")
			wordCloud = this.transform.parent.gameObject;
			//wordCloud = GameObject.FindWithTag ("EnemyCloud");
		//transform = transform;
		timeActive = 0;
		meshRenderer = GetComponent<MeshRenderer> ();
		col = meshRenderer.material.color;
		originalCol = col;
	}

	// Update is called once per frame
	void Update ()
	{
		if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) 
		{
			if (transparencyGoingUp) {
				timeActive += Time.deltaTime;
				col = meshRenderer.material.color;
				col.a = Mathf.Lerp (col.a, 0f, fadeSpeed * timeActive);
				meshRenderer.material.color = col;

				if (timeActive > fadePeriod && !startedDelay) {
					wordCloud.GetComponent<WordCloud> ().setHasFaded (false);
					delayFadeCo ();
				}

			} else if (transparencyGoingDown) {
				timeActive += Time.deltaTime;
				col = meshRenderer.material.color;
				col.a = Mathf.Lerp (originalCol.a, 1f, fadeSpeed * timeActive);
				meshRenderer.material.color = col;

				if (timeActive > fadePeriod && !startedDelay) {
					wordCloud.GetComponent<WordCloud> ().setHasFaded (true);
					delayFadeCo ();
				}
			}//end else
		}//end renderer
	}//end update

	private void delayFadeCo ()
	{
		StartCoroutine (delayFade());
	}

	private IEnumerator delayFade()
	{
		startedDelay = true;
		yield return new WaitForSecondsRealtime (1f);

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

