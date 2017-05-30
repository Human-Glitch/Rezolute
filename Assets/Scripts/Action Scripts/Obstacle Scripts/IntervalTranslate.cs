//This script translates an object at an interval with a periodic delay between translations

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalTranslate : MonoBehaviour 
{
	private bool hasStopped;
	private float translation = 0; 
	private float translationAmt = 0;
	private float originalTarget = 0;

	private bool goUpFirst;
	private bool goSidewaysFirst;
	public enum TranslationPattern
	{
		goUpFirst,
		goSidewaysFirst
	}
	public TranslationPattern translationPattern;

	public float targetTranslation = 0f;
	public float translationDelay = 0f;
	public float translationSpeed = 1f;
	//==============================================================

	//INITIALIZATION
	void Start()
	{
		initializeSettings ();
		hasStopped = false;
		originalTarget = targetTranslation;
	}

	//UPDATE once per frame
	void Update ()
	{
		translationAmt = translationSpeed * Time.deltaTime;
		upDownPattern ();
	}

	//PATTERNS
	//==============================================================
	private void upDownPattern()
	{
		//increment translation until it passes target
		if (translation < targetTranslation) 
		{
			//Debug.Log ("Entered Normal");
			if (goUpFirst == true) {transform.Translate (0, translationAmt, 0);}
			else if (goSidewaysFirst == true){transform.Translate (translationAmt, 0, 0);}
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
					else if (goSidewaysFirst == true){transform.Translate (-translationAmt, 0, 0);}

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


	//COROUTINES
	//=======================================================
	private void delayTranslation()
	{
		StartCoroutine ("delayTranslationCo");
	}

	private IEnumerator delayTranslationCo()
	{
		hasStopped = true;
		yield return new WaitForSeconds (translationDelay);

		if (Mathf.Abs(targetTranslation) > 0f) 
		{
			//Debug.Log ("Target now 0");
			targetTranslation = 0f;
		} else {
			//Debug.Log ("Target back to original");
			targetTranslation = originalTarget;
			translation = 0;
		}
		//Debug.Log ("Block Stopped");
		hasStopped = false;
	}


	//FUNCTIONS
	//==============================================================
	//Initialize inspector settings into the script
	private void initializeSettings()
	{
		if(translationPattern == TranslationPattern.goUpFirst)
		{
			goUpFirst = true;
			goSidewaysFirst = false;
		}else if (translationPattern == TranslationPattern.goSidewaysFirst)
		{
			goUpFirst = false;
			goSidewaysFirst = true;
		}
	}//end function

	//Passes in initialized values from <CreateScannerType> 
	public void initScannerTranslationPattern(bool goUpFirst, bool goSidewaysFirst, 
		float targetTranslation, float translationSpeed, float translationDelay)
	{
		this.targetTranslation = targetTranslation;
		this.translationSpeed = translationSpeed;
		this.translationDelay = translationDelay;

		if (goUpFirst == true)
		{
			translationPattern = TranslationPattern.goUpFirst;
		}
		else if(goSidewaysFirst == true)
		{
			translationPattern = TranslationPattern.goSidewaysFirst;
		}
	}//end function

}//end class
