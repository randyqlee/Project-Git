using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeck : MonoBehaviour {


	public List<HeroAsset> heroAssets;

	public PlayerDeck playerDeck;

	// Use this for initialization
	void Start () {

		 GetComponent<Deck>().heroes.Clear();
		 playerDeck = GameObject.FindObjectOfType<PlayerDeck>();
		 foreach (HeroAsset heroAsset in playerDeck.heroAssets)
		{
			GetComponent<Deck>().heroes.Add(heroAsset);

			heroAssets.Add(heroAsset);
		}
		


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
