using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScannerType : MonoBehaviour 
{
	private bool isPlantedPattern;
	private bool isActivePattern;

	public enum MovementPattern
	{
		isPlantedPattern,
		isActivePattern
	}


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
	private GameObject plantedScanner;
	private GameObject activeScanner;

	//keep a list of scanners for each trigger
	private List<GameObject> lvl2Scanner;

	// Use this for initialization
	void Start () 
	{ 
		setEnumSettings ();

		xTriggered = 0;
		spawnPoint = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if (startUpdate == true) {
			//var position = plantedBoss.transform.position.y;
			//plantedBoss.transform.Translate (0, .01f , 0);

			if (isPlantedType == true) {
				iTween.MoveUpdate (plantedScanner, 
					iTween.Hash ("x", (transform.position.x + 20), 
						"y", transform.position.y + 5, 
						"time", hashTime,
						"delay", hashDelay, 
						"onupdate", " myUpdateFunction",
						"oncomplete", "Destroy"//, 
						//"looptype", iTween.LoopType.loop
					)
				);	
			}// end planted type

			if (isActiveType == true) {
				iTween.MoveUpdate (activeScanner, 
					iTween.Hash ("x", (transform.position.x - 40), 
						"y", transform.position.y + 5, 
						"time", hashTime,
						"delay", hashDelay, 
						"onupdate", " myUpdateFunction",
						"oncomplete", "Destroy"//, 
						//"looptype", iTween.LoopType.loop
					)
				);	
			}//end activetype


		}//end start update
		*/
	}//end update

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			spawnScannerWithSettings ();
		} // end Enter2DTrigger
	} //end class

	private void spawnScannerWithSettings()
	{
		if(isPlantedPattern == true && xTriggered < 1 && isRedScanner)
		{
			plantedScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			plantedScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPlantedPattern, isActivePattern, hashTime, hashDelay);

			xTriggered++;
			//addScanner2List (plantedScanner);
			//startUpdate = true;
		}

		if(isPlantedPattern == true && xTriggered < 1 && isBlueScanner)
		{
			plantedScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
				transform.rotation) as GameObject;
			plantedScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPlantedPattern, isActivePattern, hashTime, hashDelay);

			xTriggered++;
		}

		if (isActivePattern == true && isBlueScanner) 
		{
			activeScanner = Instantiate (blueScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 5, transform.position.z), 
				transform.rotation) as GameObject;
			activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPlantedPattern, isActivePattern, hashTime, hashDelay);
		}

		if (isActivePattern == true && isRedScanner) 
		{
			activeScanner = Instantiate (redScanner, 
				new Vector3(transform.position.x + 20, transform.position.y + 5, transform.position.z), 
				transform.rotation) as GameObject;
			activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPlantedPattern, isActivePattern, hashTime, hashDelay);

		}
	}

	private void setEnumSettings()
	{
		if(movementPattern == MovementPattern.isPlantedPattern)
		{
			isPlantedPattern = true;
			isActivePattern = false;
		}else if (movementPattern == MovementPattern.isActivePattern)
		{
			isPlantedPattern = false;
			isActivePattern = true;
		}

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
