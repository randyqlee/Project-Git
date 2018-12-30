using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollection : MonoBehaviour {


	public GameObject allHeroesPanelContent;

	public HeroAsset[] allHeroAssets;

	public List<HeroAsset> playerDeckHeroAssets;



	// Use this for initialization
	void Start () {

		allHeroAssets = Resources.LoadAll <HeroAsset> ("SO Assets/Hero/Final");

		GetComponentInChildren<AllHeroesList>().InitializeHeroPanel();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveDeck()
	{
		foreach (HeroAsset heroAsset in playerDeckHeroAssets)
		{
			PlayerDeck.heroAssets.Add(heroAsset);
		}

	}
}
