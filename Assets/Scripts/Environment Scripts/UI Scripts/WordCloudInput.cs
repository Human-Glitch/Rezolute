using Assets.Scripts.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script always sets the text in the next message in <WordCloud> script
/// </summary>
public class WordCloudInput : MonoBehaviour 
{
    #region Members

    public GameObject messageCloud;
	public bool shouldClearCloud;
	private bool doneOnce;

    #endregion Members

    #region Properties

	public List<string> Messages { get; set;} = new List<string>();

    #endregion Properties

    void Start()
	{
		if(transform.CompareTag(Consts.ObjectTags.ENEMY) 
			|| transform.CompareTag(Consts.ObjectTags.BOSS) 
			|| transform.CompareTag(Consts.ObjectTags.BLUE_SCANNER) 
			|| transform.CompareTag(Consts.ObjectTags.RED_SCANNER))
		{
			messageCloud = transform.Find(Consts.WordCloudTags.MESSAGE_CLOUD).gameObject;
		
			messageCloud.GetComponent<WordCloud>().GenerateWordInCloudCo(Messages);
			doneOnce = true;
		}
		else
		{ 
			Debug.Log ("No need to send new messages to the cloud");
		}
	}

    #region Triggers

    /// <summary>
	/// Send messages to the word cloud when player detected
	/// </summary>
	/// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
	{
		try
		{
			if (other.CompareTag(Consts.ObjectTags.PLAYER) && !doneOnce && Messages.Count != 0)
			{
				if (shouldClearCloud)
				{
					messageCloud.GetComponent<WordCloud>().ClearCloud();
				}

				messageCloud.GetComponent<WordCloud>().GenerateWordInCloudCo(Messages);
				doneOnce = true;
			}
			else
			{ 
				Debug.Log ("No need to send new messages to the cloud");
			}
		}
		catch(Exception ex)
		{
			Debug.Log(ex.Message);
		}
		
	}

	#endregion Triggers

    
}
