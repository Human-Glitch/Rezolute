using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScannerType : MonoBehaviour 
{
	private float time;
	private int xTriggered;
	private bool timeStarted = false;

	public float waitTime = 0;

	//ENUMS
	private bool isPatrolPattern;
	private bool isActivePattern;
	public enum MovementPattern
	{
		isPatrolPattern,
		isActivePattern
	}

	private bool goUpFirst;
	private bool goSidewaysFirst;
	public enum TranslationPattern
	{
		goUpFirst,
		goSidewaysFirst
	}

	private bool isRedScanner;
	private bool isBlueScanner;
	public enum ScannerType
	{
		isRedScanner,
		isBlueScanner
	}
		
	[Header("Scanner Prefabs")]
	public GameObject redScanner;
	public GameObject blueScanner;

	[Header("Scanner Settings")]
	public ScannerType scannerType;
	public MovementPattern movementPattern;
	public TranslationPattern translationPattern;

	[Header("Initialize Movement Attributes")]
	public float hashTime;
	public float hashDelay;

	[Header("Initialize Translation Attributes")]
	public float targetTranslation;
	public float translationSpeed;
	public float translationDelay;

	private Vector3 spawnPoint;

	//GameObjects for the prefabs to go into
	private GameObject patrolScanner;
	private GameObject activeScanner;

	//keep a list of scanners for each trigger
	private List<GameObject> lvl2Scanner;

	// Use this for initialization
	void Start () 
	{ 
		time = 0;
		setEnumSettings ();

		xTriggered = 0;
		spawnPoint = gameObject.transform.position;
	}

	void Update()
	{
		if (timeStarted) 
		{
			time += Time.deltaTime;
		}
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			//Do this once
			if (time <= waitTime && timeStarted == false) 
			{
				timeStarted = true; 
				spawnScannerWithSettings ();
			}

			//Only do this once wait time has passed;
			if (time > waitTime) 
			{
				spawnScannerWithSettings ();
				time = 0;
			}
		} // end Enter2DTrigger
	} //end class

	private void spawnScannerWithSettings()
	{
		if (isPatrolPattern == true && xTriggered < 1 && isRedScanner)
		{
			patrolScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			
			patrolScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, goUpFirst, goSidewaysFirst, 
				targetTranslation, translationSpeed, translationDelay, hashTime, hashDelay);

			xTriggered++;
		}

		if(isPatrolPattern == true && xTriggered < 1 && isBlueScanner)
		{
			patrolScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			
			patrolScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, goUpFirst, goSidewaysFirst,
				targetTranslation, translationSpeed, translationDelay, hashTime, hashDelay);

			xTriggered++;
		}

		if (isActivePattern == true && isBlueScanner) 
		{
			activeScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			
			activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, goUpFirst, goSidewaysFirst,
				targetTranslation, translationSpeed, translationDelay, hashTime, hashDelay);
		}

		if (isActivePattern == true && isRedScanner) 
		{
			activeScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			
			activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, goUpFirst, goSidewaysFirst,
				targetTranslation, translationSpeed, translationDelay, hashTime, hashDelay);
		}
	}

	private void setEnumSettings()
	{
		//MOVEMENT PATTERN
		if(movementPattern == MovementPattern.isPatrolPattern)
		{
			isPatrolPattern = true;
			isActivePattern = false;
		}else if (movementPattern == MovementPattern.isActivePattern)
		{
			isPatrolPattern = false;
			isActivePattern = true;
		}

		//MOVEMENT DIRECTION
		if(translationPattern == TranslationPattern.goUpFirst)
		{
			goUpFirst = true;
			goSidewaysFirst = false;
		}else if (translationPattern == TranslationPattern.goSidewaysFirst)
		{
			goUpFirst = false;
			goSidewaysFirst = true;
		}

		//SCANNER TYPE
		if(scannerType == ScannerType.isRedScanner)
		{
			isRedScanner = true;
			isBlueScanner = false;
		}else if (scannerType == ScannerType.isBlueScanner)
		{
			isRedScanner = false;
			isBlueScanner = true;
		}
	}//end function

}
