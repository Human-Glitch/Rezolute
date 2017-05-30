//This controls the movement of the text in order to look like rolling credits

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour 
{
	public float speed = 0.2f;
	public bool crawling = false;

	public MainMenu menu;
	public RectTransform text;
	//==============================================================

	// Use this for initialization
	void Start () 
	{
		menu = GetComponent<MainMenu> ();
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
		text.transform.Translate(0f, Time.deltaTime * speed, 0f);

		if (gameObject.transform.position.y > 5700)
		{
			crawling = false;
			menu.MenuSelect ();
		}
	}
}
