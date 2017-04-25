using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour 
{
	public float speed = 0.2f;
	public bool crawling = false;
	// Use this for initialization
	void Start () 
	{
//		GUIText tc = GetComponent<GUIText>();
//		string creds = "Executive Producer:\nMr. Moneybags\n";
//		creds += "Art Director:\nArt Guy\n";
//		creds += "Technical Director:\nJohn Yaya\n";
//		creds += "Programming:\nPoindexter Kopnik\n";
//		creds += "Level Design:\nRandy Bugger\n";

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!crawling)
			return;
		transform.Translate(Vector3.up * Time.deltaTime * speed);
		if (gameObject.transform.position.y > 30)
		{
			crawling = false;
		}
	}
}
