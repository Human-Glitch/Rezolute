using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour 
{
	public GameObject pauseMenu;
	private bool isActive = false;
	private bool isGameOver = false;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) && !isActive && !isGameOver)
		{
			pauseMenu.SetActive (true);
			Time.timeScale = 0;
			isActive = true;
		}else if (Input.GetKeyDown(KeyCode.Escape) && isActive && !isGameOver)
		{
			pauseMenu.SetActive (false);
			Time.timeScale = 1;
			isActive = false;
		}
	}

	public void setIsGameOver (bool isGameOver)
	{
		this.isGameOver = isGameOver;
	}
}
