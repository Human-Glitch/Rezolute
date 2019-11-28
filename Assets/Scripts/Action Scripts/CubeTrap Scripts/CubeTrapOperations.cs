//This script manages the operations of the cube trap 
//These include: When to fold, When to manipulate the infinitely scrolling background, When to rotate,
//When to activate each enemy over time, when to trace the path, and when to deactivate

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrapOperations : MonoBehaviour 
{
	public GameObject FoldingCubeTrap;
	public List<GameObject> bugList;
	public GameObject backgroundLayer;
	public GameObject enemySpawnBeam;

	private Fade [] fadeScripts;

	private int cubeRotationCount = 0;
	private bool isInitialized = false;
	private int initialSpawnOffset;
	private int count = 0;

	public int activeQueue;
	public int newestSpawnOffset;
	//==============================================================

	// INITIALIZATION
	void Start () 
	{
		//gameObject.transform.GetChild (0).localScale = gameObject.transform.GetChild (0).localScale / 100f;
		initialSpawnOffset = newestSpawnOffset;
		activeQueue = 0;
	}

	//UPDATE ONCE PER FRAME
	void Update()
	{
		if (bugList != null && !isInitialized)
		{
			foreach (var bug in bugList) 
			{
				Debug.Log ("Bug deactivated");
				bug.SetActive (false);
			}
			isInitialized = true;
		}//end bad code

		setActiveEnemies ();
	}

//FUNCTIONS
//==============================================================
	public void activateTrap()
	{
		//Change to more intense music
		GameObject.FindWithTag ("Player").GetComponent<AudioManager> ().playNextSong ();

		//Find objects along the vertexes of this object and activate the <FoldMe> Scripts
		FoldingCubeTrap.transform.Find("Cube Trap").transform.Find("Vertex1").GetComponent<FoldMe> ().enabled = true;
		FoldingCubeTrap.transform.Find("Cube Trap").transform.Find ("Vertex2").GetComponent<FoldMe> ().enabled = true;
		FoldingCubeTrap.transform.Find("Cube Trap").transform.Find ("Vertex2").transform.Find("Vertex3").GetComponent<FoldMe> ().enabled = true;

		//Start TracePath script and change background motion at the same time
		FoldingCubeTrap.GetComponent<TracePath>().enabled = true;

		//Change background speed once cube starts folding
		backgroundLayer.GetComponent<movingBackgroundVertical>().speed = 1;

		//Start Coroutine
		newDelay ();
	}

	public void deactivateTrap()
	{
		fadeScripts = FoldingCubeTrap.GetComponentsInChildren<Fade>();
		foreach (Fade aScript in fadeScripts) 
		{
			aScript.enabled = true;
		}
		revertBackgroundChanges ();
		delayDestruction ();
	}
	//==============================================================
	private void triggerBackgroundScriptChanges()
	{
		var script = backgroundLayer.GetComponent<LightSpeedBackground> ();
		script.enabled = true;
		script.rotate90Degrees ();
	}

	private void revertBackgroundChanges()
	{
		var script = backgroundLayer.GetComponent<LightSpeedBackground> ();
		script.rotateReverse ();
	}
	//==============================================================
	private void setActiveEnemies()
	{
		if (activeQueue < ((bugList.Count)) && isInitialized) 
		{
			cubeRotationCount = FoldingCubeTrap.GetComponent<IntervalRotate>().RotationCount;
		
			if (cubeRotationCount > 0 && (cubeRotationCount % newestSpawnOffset == 0) 
				&& bugList [activeQueue].activeSelf == false)
			{
				bugList [activeQueue].SetActive (true);

				//Create a spawn beam where an enemy is created
				var newEnemyBeam = Instantiate (enemySpawnBeam, new Vector3 
					(bugList [activeQueue].transform.position.x,
					bugList [activeQueue].transform.position.y, 
					bugList [activeQueue].transform.position.z), 
					bugList [activeQueue].transform.rotation) as GameObject;
				newEnemyBeam.transform.SetParent (bugList [activeQueue].transform);
				//gameObject.transform.SetParent (newEnemyBeam.transform);

				newestSpawnOffset += initialSpawnOffset;
				activeQueue++;
			}//end if
		}//end if
	}//end function

//TRIGGERS
//==============================================================
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player" && count < 1) 
		{
			Debug.Log ("Trap Started");
			activateTrap ();
			count++;
		}
	}

//COROUTINES
//==============================================================
	private void newDelay () {StartCoroutine ("newDelayCo");}
	private void delayDestruction(){StartCoroutine ("delayDestructionCo");}

	private IEnumerator newDelayCo()
	{
		yield return new WaitForSecondsRealtime (15);
		FoldingCubeTrap.transform.GetComponent<IntervalRotate> ().enabled = true;

		triggerBackgroundScriptChanges ();
	}
		
	private IEnumerator delayDestructionCo()
	{
		yield return new WaitForSecondsRealtime (15);
		Destroy (FoldingCubeTrap);
	}
}
