//This script is a container for the container for prefab 3D text objects that have TextMeshes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCloud : MonoBehaviour 
{
	public List <GameObject> messageCloud;
	public GameObject nextMessage;
	public float cloudRadius;

	private bool startedDelay = false;
	private int counter = 0;

	private const int max = 3;

	private bool isDelayed = false;

	// Use this for initialization
	void Start () 
	{
		//generateWordInCloud ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (messageCloud.Count != 0 && messageCloud.Count <= max ) 
		{
			if (Vector3.Distance (messageCloud [counter - 1].transform.parent.transform.position, 
				gameObject.transform.position) > cloudRadius)
			{
				messageCloud [counter - 1].transform.localPosition += messageCloud [counter - 1].transform.position * Time.deltaTime;
			} else 
			{
				if (startedDelay == false)
					delayNewDirectionCo ();
			}//end if else
		}//end null check

	}//end update

	//COROUTINES
	private void delayNewDirectionCo (){ StartCoroutine ("delayNewDirection"); }

	private IEnumerator delayNewDirection()
	{
		startedDelay = true;
		yield return new WaitForSecondsRealtime (5f);

		foreach (GameObject word in messageCloud) 
		{
			Debug.Log ("New Delay");
	
			word.transform.position = Vector3.MoveTowards (word.transform.position, Random.insideUnitSphere * cloudRadius
				+ transform.position, cloudRadius);
			
			yield return new WaitForSecondsRealtime(2f);
		}
		startedDelay = false;
	}

	public void generateWordInCloud(List<string> messages)
	{
		StartCoroutine (delayCreation (messages));
	}//end function

	private IEnumerator delayCreation(List<string> messages)
	{
		foreach (var text in messages) 
		{
			GameObject messageObj = Instantiate (this.nextMessage, Random.insideUnitSphere * cloudRadius
				+ transform.position, transform.rotation) as GameObject;

			messageObj.GetComponent<TextMesh> ().text = text;

			messageCloud.Add (messageObj);
			messageObj.transform.parent = gameObject.transform;

			counter++;
			yield return new WaitForSecondsRealtime(2);

			isDelayed = false;
		}
	}

	//FUNCTIONS
	public void clearCloud()
	{
		if (messageCloud.Count != 0) 
		{
			foreach (var message in messageCloud) { Destroy (message); counter--; }
			Debug.Log ("Messages Destroyed.");
			messageCloud.Clear ();
		}//end if
	}
}