using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		foreach (HeroManager hero in Players[0].GetComponentsInChildren<HeroManager>())
		{
			var image = hero.glow.GetComponent<Image>().color;
			image.a = 1f;
			hero.glow.GetComponent<Image>().color = image;


		}

	}
	
	// Update is called once per frame
	void Update () {

	
	}



	public void Attack (HeroManager attacker, HeroManager defender)
	{
		int atk_damage = attacker.attack - defender.defense;
		int def_damage = defender.attack - attacker.defense;

		Debug.Log (attacker.gameObject.name + " is attacking: " + defender.gameObject.name + " for " + atk_damage + " hitpoints!");	

		defender.maxHealth = defender.maxHealth - atk_damage;

		attacker.maxHealth = attacker.maxHealth - def_damage;

		//display damage in UI

		defender.DisplayDamageText(atk_damage);
		attacker.DisplayDamageText(def_damage);

		
		CheckHealth ();
		DeselectAllHeroes ();
		NextTurn();
	}



	void CheckHealth ()
	{
		foreach (Player player in Players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				if (hero.maxHealth < 0)
					{
						Debug.Log ("Destroying " + hero.name);
						Destroy (hero.gameObject);
					}
				hero.UpdateUI();

			}
		}
	}

	public void DeselectAllHeroes ()
	{
		foreach (Player player in Players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				if (hero.isSelected)
					{
						hero.DeselectHero();
					}
				

			}
		}
	}

	void NextTurn ()
	{

		if (Players[0].isActive)
				{
					Players[0].isActive = false;
					Players[1].isActive = true;

					foreach (HeroManager hero in Players[0].GetComponentsInChildren<HeroManager>())
					{
						var image = hero.glow.GetComponent<Image>().color;
						image.a = 0f;
						hero.glow.GetComponent<Image>().color = image;


					}
					foreach (HeroManager hero in Players[1].GetComponentsInChildren<HeroManager>())
					{
						var image = hero.glow.GetComponent<Image>().color;
						image.a = 1f;
						hero.glow.GetComponent<Image>().color = image;


					}
				}
			else {
					Players[1].isActive = false;
					Players[0].isActive = true;
					foreach (HeroManager hero in Players[0].GetComponentsInChildren<HeroManager>())
					{
						var image = hero.glow.GetComponent<Image>().color;
						image.a = 1f;
						hero.glow.GetComponent<Image>().color = image;


					}
					foreach (HeroManager hero in Players[1].GetComponentsInChildren<HeroManager>())
					{
						var image = hero.glow.GetComponent<Image>().color;
						image.a = 0f;
						hero.glow.GetComponent<Image>().color = image;


					}
			}

	}


}
