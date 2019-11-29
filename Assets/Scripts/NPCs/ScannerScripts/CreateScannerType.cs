using System.Collections.Generic;
using UnityEngine;
using static TranslationEnums;

/// <summary>
/// Create scanner type.
/// </summary>
public class CreateScannerType : MonoBehaviour 
{
	private float time;
	private int xTriggered;
	private bool timeStarted = false;

    public float waitTime = 0;

    #region ENUMS

    private bool isPatrolPattern;
	private bool isActivePattern;
	public enum MovementPattern
	{
		isPatrolPattern,
		isActivePattern
	}

	private bool isRedScanner;
	private bool isBlueScanner;
	public enum ScannerType
	{
		isRedScanner,
		isBlueScanner
	}

    #endregion ENUMS

    [Header("Scanner Prefabs")]
	public GameObject redScanner;
	public GameObject blueScanner;

	[Header("Scanner Settings")]
	public ScannerType scannerType;
	public MovementPattern movementPattern;
    public TranslationPattern selectedTranslationPattern;

	[Header("Initialize Movement Attributes")]
	public float hashTime;
	public float hashDelay;

	private Vector3 spawnPoint;

	//GameObjects for the prefabs to go into
	private GameObject patrolScanner;
	private GameObject activeScanner;

	void Start () 
	{ 
		time = 0;
		SetEnumSettings ();

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

    #region TRIGGERS

    void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player")
		{
			//Do this once
			if (time <= waitTime && timeStarted == false) 
			{
				timeStarted = true; 
				SpawnScannerWithSettings ();
			}

			//Only do this once wait time has passed;
			if (time > waitTime) 
			{
				SpawnScannerWithSettings ();
				time = 0;
			}
		} 
	}

    #endregion TRIGGERS

    #region FUNCTIONS

    private void SpawnScannerWithSettings()
	{
		if (isPatrolPattern == true && xTriggered < 1 && isRedScanner)
		{
			patrolScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			
			patrolScanner.GetComponent<ScannerPattern>().Initialize(spawnPoint, isPatrolPattern, isActivePattern, selectedTranslationPattern,  hashTime, hashDelay);

			xTriggered++;
		}

		if(isPatrolPattern == true && xTriggered < 1 && isBlueScanner)
		{
			patrolScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			
			patrolScanner.GetComponent<ScannerPattern>().Initialize(spawnPoint, isPatrolPattern, isActivePattern, selectedTranslationPattern,
				 hashTime, hashDelay);

			xTriggered++;
		}

		if (isActivePattern == true && isBlueScanner) 
		{
			activeScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			
			activeScanner.GetComponent<ScannerPattern>().Initialize(spawnPoint, isPatrolPattern, isActivePattern, selectedTranslationPattern,
				 hashTime, hashDelay);
		}

		if (isActivePattern == true && isRedScanner) 
		{
			activeScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			
			activeScanner.GetComponent<ScannerPattern>().Initialize (spawnPoint, isPatrolPattern, isActivePattern, selectedTranslationPattern,
                hashTime, hashDelay);
		}
	}

	private void SetEnumSettings()
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
	}

    #endregion FUNCTIONS
}
