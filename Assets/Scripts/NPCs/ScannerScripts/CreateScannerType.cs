using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScannerType : MonoBehaviour 
{
	public bool isPlantedType;
	public bool isActiveType;
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
		xTriggered = 0;
		spawnPoint = gameObject.transform.position;
		//lvl2Scanner = new List<GameObject> ();
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
			if(isPlantedType == true && xTriggered < 1)
			{
				plantedScanner = Instantiate (redScanner, 
					new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z), 
					transform.rotation) as GameObject;
				plantedScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPlantedType, isActiveType, hashTime, hashDelay);

				xTriggered++;
				//addScanner2List (plantedScanner);
				//startUpdate = true;
			}

			if (isActiveType == true) 
			{
				activeScanner = Instantiate (blueScanner, 
					new Vector3(transform.position.x + 20, transform.position.y + 5, transform.position.z), 
					transform.rotation) as GameObject;
				activeScanner.GetComponent<ScannerPattern> ().Initialize (spawnPoint, isPlantedType, isActiveType, hashTime, hashDelay);

				//addScanner2List (activeScanner);
			}
		}
	}

	/*
	private void addScanner2List(GameObject scanner)
	{
			lvl2Scanner.Add(scanner);
	}

	private GameObject returnNewestScannerPrefab()
	{
		return lvl2Scanner [lvl2Scanner.Count - 1];
	}
	*/

}
