using System.Collections.Generic;
using UnityEngine;
using static Enums;

/// <summary>
/// Create scanner type.
/// </summary>
public class CreateScannerType : MonoBehaviour 
{
	private float time;
	private int xTriggered;
	private bool timeStarted = false;

    public float waitTime = 0;

    [Header("Scanner Prefabs")]
	public GameObject redScanner;
	public GameObject blueScanner;

	[Header("Scanner Settings")]
	public ScannerType selectedScannerType;
	public MovementPattern selectedMovementPattern;
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
		if (selectedMovementPattern == MovementPattern.Patrol && xTriggered < 1 && selectedScannerType == ScannerType.RedScanner)
		{
			patrolScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			
			patrolScanner.GetComponent<ScannerPattern>().Initialize(spawnPoint, selectedMovementPattern, selectedTranslationPattern,  hashTime, hashDelay);

			xTriggered++;
		}

		if(selectedMovementPattern == MovementPattern.Patrol && xTriggered < 1 && selectedScannerType == ScannerType.BlueScanner)
		{
			patrolScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			
			patrolScanner.GetComponent<ScannerPattern>().Initialize(spawnPoint, selectedMovementPattern, selectedTranslationPattern,
				 hashTime, hashDelay);

			xTriggered++;
		}

		if (selectedMovementPattern == MovementPattern.Active && selectedScannerType == ScannerType.BlueScanner) 
		{
			activeScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			
			activeScanner.GetComponent<ScannerPattern>().Initialize(spawnPoint, selectedMovementPattern, selectedTranslationPattern,
				 hashTime, hashDelay);
		}

		if (selectedMovementPattern == MovementPattern.Active && selectedScannerType == ScannerType.RedScanner) 
		{
			activeScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 6, transform.position.z), 
				transform.rotation) as GameObject;
			
			activeScanner.GetComponent<ScannerPattern>().Initialize (spawnPoint, selectedMovementPattern, selectedTranslationPattern,
                hashTime, hashDelay);
		}
	}

    #endregion FUNCTIONS
}
