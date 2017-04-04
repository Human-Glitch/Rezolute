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
	}

	// Update is called once per frame
	void Update () 
	{
		timeActive += Time.fixedTime;
		float fade = Mathf.SmoothDamp (spriteRenderer.color.a, 0f, ref fadeSpeed, fadeTime); //ref means value is passed in and will change over time to fade out
		spriteRenderer.color = new Color (1f, 1f, 1f, fade);	

		if (timeActive > 50000) 
		{
			//Debug.Log ("Object destroyed");
			if(gameObject.tag == "enemy")
			{
				Destroy (deathEffect);
			}else{gameObject.SetActive (false);}
		}
	}
}
