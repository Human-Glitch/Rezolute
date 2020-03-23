//This script is a container for the container for prefab 3D text objects that have TextMeshes

using Assets.Scripts.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCloud : MonoBehaviour 
{
    #region Members

    public List <GameObject> messageCloud;
	public GameObject nextMessage;
	public float cloudRadius;

	private bool startedDelay = false;
	private bool stopEverything = false;
	private int counter = 0;
	private const int max = 3;

    #endregion Members

    #region Properties

    public bool HasFaded { get; set;} = false;

    #endregion Properties

    void Start()
	{
		if (gameObject.CompareTag(Consts.WordCloudTags.PLAYER_CLOUD)) 
		{
			//clearCloud ();
		}
	}
	
	void Update ()
	{
		if(GetComponent<Renderer>().IsVisibleFrom(Camera.main) && messageCloud.Count != 0)
		{
			CalculateMessageMovement();
		}
	}

    #region Public Methods

	/// <summary>
	/// Creates the word in the cloud using info from <WordCloudInput>
	/// </summary>
	/// <param name="messages"></param>
	public void GenerateWordInCloudCo(List<string> messages)
	{
		StartCoroutine(GenerateWordInCloud(messages));
	}

    public void CalculateMessageMovement()
	{
		if (messageCloud.Count > 0 && messageCloud.Count <= max && stopEverything == false) 
		{
			if (Vector3.Distance (messageCloud [counter - 1].transform.parent.transform.position, 
				gameObject.transform.position) > cloudRadius)
			{
				messageCloud [counter - 1].transform.localPosition += messageCloud [counter - 1].transform.position * Time.deltaTime;
			} 
			else if (startedDelay == false && stopEverything == false && HasFaded)
			{
				StartCoroutine(DelayNewDirection());
			}
		}
	}

	/// <summary>
	/// Empty Cloud of messages
	/// </summary>
	public void ClearCloud()
	{
		stopEverything = true;
		if (messageCloud.Count > 0) 
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
		}
	}

    #endregion Public Methods

	#region Coroutines

    private IEnumerator GenerateWordInCloud(List<string> messages)
	{
		for(int i = messages.Count - 1; i >= 0; --i )
		{
			var text = messages[i];

			GameObject messageObj = 
				Instantiate
				(
					this.nextMessage, 
					Random.insideUnitSphere * cloudRadius + transform.position, 
					transform.rotation
				) as GameObject;

			messageObj.GetComponent<TextMesh>().text = text;

			messageCloud.Add(messageObj);
			messageObj.transform.parent = gameObject.transform;

			counter++;

			yield return new WaitForSecondsRealtime(.5f);
		}
	}

    /// <summary>
    /// Delays the new coordinates of the words after <WordCloudFade>().transparencyGoingDown is completed
    /// </summary>
    private IEnumerator DelayNewDirection()
	{
		startedDelay = true;

		if (stopEverything == true) 
		{
			startedDelay = false;
			yield break;
		}

		for(int i = messageCloud.Count - 1; i >= 0; --i )
		{
			Debug.Log ("New Delay");
			var message = messageCloud[i];
			message.transform.position = 
				Vector3.MoveTowards
				(
					message.transform.position, 
					Random.insideUnitSphere * cloudRadius + transform.position, 
					cloudRadius
				);

			//Transform position of each item after x seconds
			yield return new WaitForSecondsRealtime(.15f);
		}
		startedDelay = false;
	}

    #endregion Coroutines
}