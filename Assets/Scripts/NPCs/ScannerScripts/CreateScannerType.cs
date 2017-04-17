using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScannerType : MonoBehaviour 
{
	private float time;

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
	public TranslationPattern translationPattern;

	private bool isRedScanner;
	private bool isBlueScanner;
	public enum ScannerType
	{
		isRedScanner,
		isBlueScanner
	}

	public MovementPattern movementPattern;
	public ScannerType scannerType;

	public float hashTime;
	public float hashDelay;

	//Holds the prefabs
	public GameObject redScanner;
	public GameObject blueScanner;

	private Vector3 spawnPoint;
	private int xTriggered;

	//GameObjects for the prefabs to go into
	private GameObject patrolScanner;
	private GameObject activeScanner;

	//keep a list of scanners for each trigger
	private List<GameObject> lvl2Scanner;

	// Use this for initialization
	void Start () 
	{ 
		time += Time.deltaTime;
		setEnumSettings ();

		xTriggered = 0;
		spawnPoint = gameObject.transform.position;
	}

	void Update()
	{
		time += Time.deltaTime;
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			spawnScannerWithSettings ();
		} // end Enter2DTrigger
	} //end class

	private void spawnScannerWithSettings()
	{
		if (isPatrolPattern == true && xTriggered < 1 && isRedScanner)
		{
			patrolScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			patrolScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, hashTime, hashDelay);

			xTriggered++;
		}

		if(isPatrolPattern == true && xTriggered < 1 && isBlueScanner)
		{
			patrolScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			patrolScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, hashTime, hashDelay);

			xTriggered++;
		}

		if (isActivePattern == true && isBlueScanner) 
		{
			activeScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, hashTime, hashDelay);
		}

		if (isActivePattern == true && isRedScanner) 
		{
			activeScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPatrolPattern, isActivePattern, hashTime, hashDelay);

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
