using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]

public class FollowPlayer : MonoBehaviour 
{
	public Transform Target;

	[Header("Which Axis?")]
	public bool willTargetX;
	public bool willTargetY;
	public bool willTargetZ;

	[Header("Follow Settings")]
	public int cameraYoffset;
	public bool allowXreturn;
	public bool forceXscrolling;
	public bool forceYscrolling;
	public float scrollSpeed;

	private float furthestXpositon;
	private float posX;
	private float posY;
	private float posZ;

	private int doOnce = 0;

	void Start()
	{
		if (Target == null)
			Target = gameObject.transform;

		resetCameraPosition ();
		transform.position = Target.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () 
	{
		//Want Y axis to move with player
		if(willTargetY == true){ posY = Target.position.y; }
		else if (willTargetY == false){ posY = 0; }

		//Want Z axis to move with player
		if(willTargetZ == true){ posZ = Target.position.z; }
		else if (willTargetZ == false){ posZ = 0; }

		switch (willTargetX) 
		{
			case(true):
				updateCameraPositionOnXReturn ();
				transform.position = new Vector3 (posX, posY + cameraYoffset, Target.position.z);
				break;
			
			case(false):
				posX = 0;
				break;
		}//end switch
			
		switch(forceXscrolling)
		{
			case(true):
				willTargetX = false; // must be false for this setting to work
				gameObject.transform.Translate(new Vector3(scrollSpeed , posY, posZ)) ;
				break;

			case(false):
				break;
		}

		switch(forceYscrolling)
		{
		case(true):
			if (doOnce < 1)
			{
				transform.position = new Vector3 (Target.position.x, Target.position.y + cameraYoffset, Target.position.z);//subject to change
				doOnce++;
			}
			willTargetX = false; // must be false for this setting to work
			//resetCameraPosition ();
			gameObject.transform.Translate(new Vector3(0 , scrollSpeed, 0)) ;
			break;

		case(false):
			break;
		}
			
		//Move camera to settings
		//transform.position = new Vector3 (posX, posY + cameraYoffset, Target.position.z);
	}//end late update

	void updateCameraPositionOnXReturn()
	{
		switch(allowXreturn)
		{
			case (true):
			//Change camera position to 
				posX = Target.position.x;
				furthestXpositon = posX;
				break;

			case (false):
				//Track furthest x position of the player and update camera
				if (Target.position.x > furthestXpositon) 
				{
					furthestXpositon = Target.position.x;
					posX = furthestXpositon;
				}
				break;
		}//end switch
	}//end function

	public void resetCameraPosition()
	{
		furthestXpositon = Target.position.x;
		posX = furthestXpositon;
		transform.position = new Vector3 (posX, posY + cameraYoffset, Target.position.z);//subject to change
	}

	//MUTATOR METHODS
	public void setForceXScrolling(bool forceXscrolling)
	{
		this.forceXscrolling = forceXscrolling;
		forceYscrolling = false;
	}
	public void setForceYScrolling(bool forceYscrolling)
	{
		this.forceYscrolling = forceYscrolling;
		forceXscrolling = false;
	}
	public void setScrollSpeed(float scrollSpeed){ this.scrollSpeed = scrollSpeed; }

}//end script