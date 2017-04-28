using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour 
{
	public GameObject deathParticle;
	GameObject deathEffect;
	public float fadeSpeed = 0f;
	public float fadeTime = .5f;

	public float timeActive;

	//private Transform transform;
	private SpriteRenderer spriteRenderer;
	private AudioSource bossAudioSource;


	// Use this for initialization
	void Start () 
	{
		//transform = transform;
		timeActive = 0;
		spriteRenderer = GetComponent<SpriteRenderer> ();

		//transform.localPosition = new Vector3 (7.3f, 0f, 0f); //distance from the barrel
		//transform.rotation = cannon.rotation; //keep rotation the same as parent class
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
