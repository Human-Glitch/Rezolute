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
	private bool stopEverything = false;
	
//UPDATE
	void Update ()
	{
		if(GetComponent<Renderer>().IsVisibleFrom(Camera.main))
		{
			calculateMessageMovement ();
		}
	}//end update

//COROUTINES
	//Creates the word in the cloud using info from <WordCloudInput>
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
	}

	//Delays the new coordinates of the words after <WordCloudFade>().transparencyGoingDown
	//is completed
	private void delayNewDirectionCo (){ StartCoroutine ("delayNewDirection"); }

	private IEnumerator delayNewDirection()
	{
		startedDelay = true;

		for(int i = messageCloud.Count -1; i >= 0; --i )
		{
			if (stopEverything == true) 
			{
				yield break;
			}

			Debug.Log ("New Delay");
			var message = messageCloud [i];
			message.transform.position = Vector3.MoveTowards (message.transform.position, Random.insideUnitSphere * cloudRadius
				+ transform.position, cloudRadius);

			//Transform position of each item after x seconds
			yield return new WaitForSecondsRealtime(0f);
		}
		startedDelay = false;
	}

//FUNCTIONS
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

	//Empty Cloud of messages
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

	//Accessor Function
	public void setHasFaded(bool hasFaded){ this.hasFaded = hasFaded; }
}