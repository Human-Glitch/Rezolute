using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wordCloudFade : MonoBehaviour 
{
    #region Members

    public float fadeSpeed;
	public float fadePeriod;
	public float timeActive;

	public bool transparencyGoingUp;
	public bool transparencyGoingDown;

	private bool startedDelay;

	private GameObject wordCloud;
	private MeshRenderer meshRenderer;
	private Color color;
	private Color originalColor;

    #endregion Members

    void Start () 
	{
		if (this.transform.parent != null && transform.parent.tag == Consts.WordCloudTags.PLAYER_CLOUD)
		{
			wordCloud = this.transform.parent.gameObject;
		}
		else if(this.transform.parent != null && transform.parent.tag == Consts.WordCloudTags.ENEMY_CLOUD)
		{
			wordCloud = this.transform.parent.gameObject;
			timeActive = 0;
			meshRenderer = GetComponent<MeshRenderer> ();
			color = meshRenderer.material.color;
			originalColor = color;
		}
			
	}

	void Update ()
	{
		if (GetComponent<Renderer>().IsVisibleFrom(Camera.main)) 
		{
			if (transparencyGoingUp) 
			{
				timeActive += Time.deltaTime;
				color = meshRenderer.material.color;
				color.a = Mathf.Lerp (color.a, 0f, fadeSpeed * timeActive);
				meshRenderer.material.color = color;

				if (timeActive > fadePeriod && !startedDelay) 
				{
					wordCloud.GetComponent<WordCloud> ().HasFaded = false;
					StartCoroutine(DelayFade());
				}

			} 
			else if (transparencyGoingDown) 
			{
				timeActive += Time.deltaTime;
				color = meshRenderer.material.color;
				color.a = Mathf.Lerp(originalColor.a, 1f, fadeSpeed * timeActive);
				meshRenderer.material.color = color;

				if (timeActive > fadePeriod && !startedDelay) 
				{
					wordCloud.GetComponent<WordCloud>().HasFaded = true;
					StartCoroutine(DelayFade());
				}
			}
		}
	}

    #region Coroutines
	
	private IEnumerator DelayFade()
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

    #endregion Coroutines
}

