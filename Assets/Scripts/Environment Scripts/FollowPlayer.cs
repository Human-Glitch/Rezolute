using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	public Transform Target;

	public bool willTargetX;
	public bool willTargetY;
	public bool willTargetZ;

	public int cameraYoffset;
	public bool allowXreturn;

	private float furthestXpositon;
	private float posX;
	private float posY;
	private float posZ;

	void Start()
	{
		if (Target == null)
			Target = gameObject.transform;
		
		furthestXpositon = Target.position.x;
		transform.position = Target.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () 
	{
		switch (willTargetX) 
		{
			case(true):
				updateCameraPositionOnXReturn ();
				break;
			
			case(false):
				posX = 0;
				break;
		}//end switch

		//Want Y axis to move with player
		if(willTargetY == true){ posY = Target.position.y; }
		else if (willTargetX == false){ posY = 0; }

		//Want Z axis to move with player
		if(willTargetZ == true){ posZ = Target.position.z; }
		else if (willTargetZ == false){ posZ = 0; }

		//Move camera to settings
		transform.position = new Vector3 (posX, posY + cameraYoffset, Target.position.z);
	}//end late update

	void updateCameraPositionOnXReturn()
	{
		switch(allowXreturn)
		{
			case (true):
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
	}

}//end script