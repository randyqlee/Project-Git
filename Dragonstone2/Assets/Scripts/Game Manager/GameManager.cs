using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// static
	// initialize 2 teams for battle
	// keep track of all happenings
	//initialize other systems

	public static GameManager Instance;

	//placeholder for Player gameobjects
	public Player[] Players; 

	void Awake ()
	{

		Instance = this;
		//find all Players in the scene and store them in array
		Players = GameObject.FindObjectsOfType<Player>();


	}

	// Use this for initialization
	void Start () {

		Players[0].isActive = true;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (Players[0].isActive)
				{
					Players[0].isActive = false;
					Players[1].isActive = true;
				}
			else {
					Players[1].isActive = false;
					Players[0].isActive = true;
			}
		}
		
	}
/*
	public void DeselectOtherHeroes (HeroManager hero)
	{
		foreach (Player player in Players)
		{
			foreach (HeroManager otherHero in player.GetComponentsInChildren<HeroManager>())
			{
				if (otherHero.GetInstanceID() != hero.GetInstanceID())
				{
					if (otherHero.isSelected)
					{
						Debug.Log ("Deselecting " + otherHero.name);
						otherHero.isSelected = false;
					}

				}
				

			}
		}
	}
*/

	public void DeselectAllHeroes ()
	{
		foreach (Player player in Players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				if (hero.isSelected)
					{
						Debug.Log ("Deselecting " + hero.name);
						hero.isSelected = false;
					}
				

			}
		}
	}


}
