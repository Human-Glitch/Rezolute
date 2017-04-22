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

	//Send messages to the word cloud when player detected
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !doneOnce && messages.Count != 0) //safety checks
		{
			if (shouldClearCloud)
				messageCloud.GetComponent<WordCloud> ().clearCloud ();
		
			messageCloud.GetComponent<WordCloud> ().generateWordInCloud (getMessages ());
			doneOnce = true;
		}else{ 
			Debug.Log ("No need to send new messages to the cloud");
		}
	}//end void

}
