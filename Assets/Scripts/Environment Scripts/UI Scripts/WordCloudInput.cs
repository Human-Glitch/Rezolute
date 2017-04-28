//This script always sets the text in the next message in <WordCloud> script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCloudInput : MonoBehaviour 
{
	public GameObject messageCloud;
	public bool shouldClearCloud;
	public List<string> messages;

	private bool doneOnce;

	public List<string> getMessages(){ return messages; }

	void Start()
	{
		if(transform.tag == "Enemy" || transform.tag == "Boss" || transform.tag == "Blue Scanner" || transform.tag == "Red Scanner")
		{
			messageCloud = transform.FindChild("messageCloud").gameObject; //cbecks from parent object to find the message cloud
		
			messageCloud.GetComponent<WordCloud> ().generateWordInCloudCo (getMessages ());
			doneOnce = true;

		}else{ 
			Debug.Log ("No need to send new messages to the cloud");
		}
	}

	//Send messages to the word cloud when player detected
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !doneOnce && messages.Count != 0) //safety checks
		{
			if (shouldClearCloud)
				messageCloud.GetComponent<WordCloud> ().clearCloud ();
		
			messageCloud.GetComponent<WordCloud> ().generateWordInCloudCo (getMessages ());
			doneOnce = true;
		}else{ 
			Debug.Log ("No need to send new messages to the cloud");
		}
	}//end void



}
