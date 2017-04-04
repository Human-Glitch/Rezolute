using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalTranslate : MonoBehaviour 
{
	private bool hasStopped;
	private float translation = 0; 
	private float translationAmt = 0;
	private float originalTarget = 0;

	public bool goUpFirst;
	public float targetTranslation = 0f;
	public float translationDelay = 0f;
	public float translateSpeed = 1f;

	void Start()
	{
		hasStopped = false;
		originalTarget = targetTranslation;
	}

	void Update ()
	{
		translationAmt = translateSpeed * Time.deltaTime;
		upDownPattern ();
	}

	private void upDownPattern()
	{
		//increment translation until it passes target
		if (translation < targetTranslation) 
		{
			//Debug.Log ("Entered Normal");
			if (goUpFirst == true) {transform.Translate (0, translationAmt, 0);}
			//else if(!goUpFirst == false){transform.Translate (0, -translationAmt, 0);}
			translation += translationAmt;
		} 

		//once translation passes target and hasn't stopped, do 
		else if (translation > targetTranslation)
		{
			if (hasStopped == false) 
			{
				if (targetTranslation == 0) 
				{
					//Debug.Log ("Entered reverse");
					if (goUpFirst == true) {transform.Translate (0, -translationAmt, 0);}
					//else if(!goUpFirst == false){transform.Translate (0, translationAmt, 0);}

					translation -= translationAmt;
					//Debug.Log (translation);

					//Once decrement passes target, then go to else to delay
					if(translation < targetTranslation)
					{
						//Debug.Log( "if(translation < targetTranslation)  reached");
						targetTranslation = 0f;
						delayTranslation ();
					}
					//Else block has stopped and so delay reverse until time has passed
				} else {
					//Debug.Log ("else reached");
					delayTranslation ();
				}
			}
		}
	}//end upDownPattern()

	private void delayTranslation()
	{
		StartCoroutine ("delayTranslationCo");
	}

	private IEnumerator delayTranslationCo(){
		
		hasStopped = true;
		yield return new WaitForSeconds (translationDelay);

		if (Mathf.Abs(targetTranslation) > 0f) 
		{
			//Debug.Log ("Target now 0");
			targetTranslation = 0f;
			;
		} else {
			//Debug.Log ("Target back to original");
			targetTranslation = originalTarget;
			translation = 0;
		
		}

		//Debug.Log ("Block Stopped");

		hasStopped = false;
	}


}
