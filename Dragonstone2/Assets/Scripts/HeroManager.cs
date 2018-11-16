using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Gets the Hero SO values
public class HeroManager : MonoBehaviour
{
//	public HeroAsset heroAsset;

	public string heroName;
	public Sprite image;
	
	public int maxHealth;
	public int attack;
	public int defense;
	public float chance;

	public Rarity rarity;

	public CapsuleCollider2D col;


	void Awake () {
//		if (heroAsset != null)
//			ReadHeroFromAsset();

	col = GetComponent<CapsuleCollider2D>();


	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ReadHeroFromAsset()
	{
/* 
		heroName = heroAsset.heroName;
		image = heroAsset.image;
		maxHealth = heroAsset.maxHealth;
		attack = heroAsset.attack;
		defense = heroAsset.defense;
		chance = heroAsset.chance;

		rarity = heroAsset.rarity;
*/
	}

	void OnMouseDown ()
	{
		Debug.Log("Hero clicked: " + heroName);
	}
}
