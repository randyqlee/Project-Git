using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour {

	public List<HeroAsset> heroAssets;

	void Start () {

		heroAssets = new List<HeroAsset>();
		DontDestroyOnLoad(gameObject);
		
	}

/*
properties accessor?

*/


}
