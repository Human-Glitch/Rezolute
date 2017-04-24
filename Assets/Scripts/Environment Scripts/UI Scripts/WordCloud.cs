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

	private bool hasFaded = false;
	private bool isDelayed = false;
	private bool stopEverything = false;
	
	// Update is called once per frame
	void Update ()
	{
		if(GetComponent<Renderer>().IsVisibleFrom(Camera.main))
		{
			calculateMessageMovement ();
		}


	}//end update

	//COROUTINES
	private void delayNewDirectionCo (){ StartCoroutine ("delayNewDirection"); }

	private IEnumerator delayNewDirection()
	{
		startedDelay = true;

		//Delay change in direction for x seconds
		//yield return new WaitForSecondsRealtime (1f);

		for(int i = messageCloud.Count -1; i >= 0; --i )
		{
			Debug.Log ("New Delay");
			var message = messageCloud [i];
			message.transform.position = Vector3.MoveTowards (message.transform.position, Random.insideUnitSphere * cloudRadius
				+ transform.position, cloudRadius);

			//Transform position of each item after x seconds
			yield return new WaitForSecondsRealtime(0f);
		}
		startedDelay = false;
	}

	public void generateWordInCloudCo(List<string> messages)
	{
		StartCoroutine (delayCreation (messages));
	}//end function

	private IEnumerator delayCreation(List<string> messages)
	{
		for(int i = messages.Count -1; i >= 0; --i )
		{
			var text = messages [i]; //What's does the text in a message say?

			GameObject messageObj = Instantiate (this.nextMessage, Random.insideUnitSphere * cloudRadius
				+ transform.position, transform.rotation) as GameObject;

			messageObj.GetComponent<TextMesh> ().text = text;

			messageCloud.Add (messageObj);
			messageObj.transform.parent = gameObject.transform;

			counter++;

			yield return new WaitForSecondsRealtime (.5f);
		}
			
		isDelayed = false;
	
	}

	//FUNCTIONS
	public void clearCloud()
	{
		stopEverything = true;
		if (messageCloud.Count != 0) 
		{
			int index = messageCloud.Count;

			for(int i = messageCloud.Count -1; i >= 0; --i )
			{
				var message = messageCloud [i];
				Destroy (message);
				messageCloud.RemoveAt (i);
				counter--;
			}
			Debug.Log ("Messages Destroyed.");

			stopEverything = false;
		}//end if
	}

	public void calculateMessageMovement()
	{
		if (messageCloud.Count != 0 && messageCloud.Count <= max && stopEverything == false) 
		{
			if (Vector3.Distance (messageCloud [counter - 1].transform.parent.transform.position, 
				gameObject.transform.position) > cloudRadius)
			{
				messageCloud [counter - 1].transform.localPosition += messageCloud [counter - 1].transform.position * Time.deltaTime;
			} else 
			{
				if (startedDelay == false && stopEverything == false && hasFaded)
					delayNewDirectionCo ();
			}//end if else
		}//end null check
	}//end void

	public void setHasFaded(bool hasFaded){ this.hasFaded = hasFaded; }


}