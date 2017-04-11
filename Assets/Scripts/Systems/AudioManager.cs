using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public List<AudioClip> musicList;
	private AudioSource nowPlaying;
	private int queue;

	// Use this for initialization
	void Start () 
	{
		queue = musicList.Count;
		if (musicList != null) 
		{
			nowPlaying = GetComponent<AudioSource> ();
			resetMusic ();
		}else {
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
}
