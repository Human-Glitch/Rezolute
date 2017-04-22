using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCloud : MonoBehaviour 
{
	public List <GameObject> cloudWords;
	public GameObject nextWord;
	public float cloudRadius;

	private bool startedDelay = false;
	private int counter = 0;

	// Use this for initialization
	void Start () 
	{
		GameObject word = Instantiate (nextWord, Random.insideUnitSphere * cloudRadius
			+ transform.position , transform.rotation) as GameObject;
		cloudWords.Add (word);
		word.transform.parent = gameObject.transform;
		counter++;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			GameObject word = Instantiate (nextWord, Random.insideUnitSphere * cloudRadius
			                  + transform.position, transform.rotation) as GameObject;
			cloudWords.Add (word);
			word.transform.parent = gameObject.transform;

			counter++;
		}


		if (Vector3.Distance (cloudWords[counter - 1].transform.parent.transform.position, gameObject.transform.position) > cloudRadius) {
				
				cloudWords[counter -1].transform.localPosition += cloudWords[counter -1].transform.position.normalized * Time.deltaTime;
			} else {
				if (startedDelay == false)
					delayNewDirectionCo ();
			}


		//cloudWords[0].transform.position = sVector3.Lerp(transform.position, vector, 1f * Time.deltaTime)

	}
	private void delayNewDirectionCo ()
	{
		StartCoroutine ("delayNewDirection");
	}

	private IEnumerator delayNewDirection()
	{
		startedDelay = true;
		yield return new WaitForSecondsRealtime (4f);

		foreach (GameObject word in cloudWords) 
		{
			Debug.Log (Time.fixedTime);
			word.transform.position = Vector3.MoveTowards (word.transform.position, Random.insideUnitSphere * cloudRadius
			+ transform.position, cloudRadius);

			//yield return new WaitForSecondsRealtime (.5f);
		}
		startedDelay = false;
	}
}