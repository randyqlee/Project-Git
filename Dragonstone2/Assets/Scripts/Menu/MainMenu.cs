﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


	public void PlayGame()
	{
		SceneManager.LoadScene("BattleScene");

	}

	public void Options()
	{
		SceneManager.LoadScene("BattleScene");

	}

	public void Quit()
	{

		Application.Quit();

	}





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
