using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public List<AudioClip> musicList;
	private AudioSource nowPlaying;
	private int queue = 0;

	// Use this for initialization
	void Start () 
	{
		//if(musicList.Count != 0 && musicList.Count > 0)
	
		if (musicList.Count != 0) 
		{
			queue = musicList.Count;
			nowPlaying = GetComponent<AudioSource> ();
			resetMusic ();
		} else if (GetComponent<AudioSource> () != null)
			nowPlaying = GetComponent<AudioSource> ();
		else{
			Debug.Log ("No Audio Source is attached to this {0}", gameObject);
		}
	}

	public void resetMusic()
	{
		queue = 0;
		nowPlaying.clip = musicList[queue];
		nowPlaying.Play ();
	}

	public void playNextSong()
	{
		if (musicList != null && queue < musicList.Count) 
		{
			nowPlaying.Stop ();
			queue++;
			nowPlaying.clip = musicList [queue];
			nowPlaying.Play ();
		}else {Debug.Log ("There is no music in the queue");}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "MainCamera")
		{
			nowPlaying.Play ();
		}
	}
}
