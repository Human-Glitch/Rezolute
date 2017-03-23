using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalTranslate : MonoBehaviour 
{
	private bool hasStopped;
	private float translation = 0; 
	private float translationAmt = 0;
	private float originalTarget = 0;

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

		if (Mathf.Abs (translation) < targetTranslation) 
		{
			//Debug.Log ("Entered Normal");
			transform.Translate (0, translationAmt, 0);
			translation += translationAmt;
		} 

		if (Mathf.Abs(translation) > targetTranslation)
		{
			if (hasStopped == false) 
			{
				if (targetTranslation == 0) 
				{
					//Debug.Log ("Entered reverse");
					transform.Translate (0, -translationAmt, 0);
					translation -= translationAmt;
					//Debug.Log (translation);

					if(translation < targetTranslation)
					{
						targetTranslation = 0f;
						delayTranslation ();
					}
				} else {
					//Debug.Log ("false reached");
					targetTranslation = Mathf.Round (targetTranslation);
					delayTranslation ();
				}
			}



			/*
			if (hasStopped == true) 
			{
				Debug.Log ("true reached");
				transform.Translate (0, -translationAmt, 0);
				translation += translationAmt;
			}*/
		}
	}

	public void delayTranslation()
	{
		StartCoroutine ("delayTranslationCo");
	}

	public IEnumerator delayTranslationCo(){
		
		hasStopped = true;
		




		yield return new WaitForSeconds (translationDelay);
		if (targetTranslation > 0f) {
			targetTranslation = 0f;
		} else {
			targetTranslation = originalTarget;
		}
		Debug.Log ("Block Stopped");

		hasStopped = false;
	}

}
