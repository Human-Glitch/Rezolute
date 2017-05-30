//This will fade an object over time and then destroy it

using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour 
{
	public GameObject deathParticle;
	GameObject deathEffect;
	public float fadeSpeed = 0f;
	public float fadeTime = .5f;

	public float timeActive;

	private SpriteRenderer spriteRenderer;
	private AudioSource bossAudioSource;
	//==============================================================

	// INITIALIZATION
	void Start () 
	{
		timeActive = 0;
		spriteRenderer = GetComponent<SpriteRenderer> ();

		spriteRenderer.color = new Color (1f, 1f, 1f, 1f);

		if(gameObject.tag == "Enemy")
		{
			deathEffect = Instantiate (deathParticle, transform.parent.position, Quaternion.identity, transform.parent) as GameObject;
			gameObject.SetActive (false);
		}

		if(gameObject.tag == "Boss")
		{
			deathEffect = Instantiate (deathParticle, transform.position, Quaternion.identity, transform.parent) as GameObject;
			bossAudioSource = gameObject.GetComponent<AudioSource> ();
			bossAudioSource.Play ();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		timeActive += Time.deltaTime;
		float fade = Mathf.SmoothDamp (spriteRenderer.color.a, 0f, ref fadeSpeed, fadeTime); //ref means value is passed in and will change over time to fade out
		spriteRenderer.color = new Color (1f, 1f, 1f, fade);	

		if (timeActive > 5) 
		{
			//Debug.Log ("Object destroyed");
			if(gameObject.tag == "enemy" || gameObject.tag == "Boss")
			{
				if(gameObject.tag == "Boss")
				{
					Destroy (gameObject);
				}
				Destroy (deathEffect);

			}else if (timeActive > 980){
				gameObject.SetActive (false);
			}

		}
	}
}
