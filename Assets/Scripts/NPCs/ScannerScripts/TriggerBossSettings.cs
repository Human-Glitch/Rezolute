using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossSettings : MonoBehaviour 
{
	private GameObject boss;
	private GameObject mainCamera;
	private bool shouldScroll = false;

	public bool shouldXscroll = false;
	public bool shouldYscroll = false;

	public bool shouldRotateBoss = false;

	void Start()
	{
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!shouldScroll) 
		{
			if (other.tag == "Player") 
			{
				Debug.Log("Boss Scrolling Activated");
				var script = mainCamera.GetComponent<FollowPlayer> ();

				//Set Scroll Direction
				if(shouldXscroll)
					script.setForceXScrolling (true);
				else if(shouldYscroll)
					script.setForceYScrolling (true);

				//How should the Boss be positioned after rotating camera
				if (shouldRotateBoss)
				{
					mainCamera.GetComponent<LightSpeedBackground> ().enabled = true;
					script.setScrollSpeed (.04f);

					boss = GameObject.FindWithTag ("Boss");
					boss.GetComponent<LightSpeedBackground> ().enabled = true;
					boss.transform.Translate (0, -15, 0);
				}

				shouldScroll = true;
			}//endif
		}//endif

	}//end trigger

}
