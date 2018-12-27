using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHeroesList : MonoBehaviour {

	public List<GameObject> itemList;

	public List<HeroAsset> allHeroAssets;

	public GameObject myCollection;

	public GameObject heroIcon;

	// Use this for initialization
	void Start () {

		allHeroAssets = myCollection.GetComponent<MyCollection>().allHeroAssets;

		foreach (HeroAsset heroAsset in allHeroAssets)
		{
			
			GameObject heroGO = Instantiate (heroIcon); 
			heroGO.GetComponent<HeroIcon>().heroAsset = heroAsset;
			heroGO.transform.SetParent(gameObject.transform);
			itemList.Add(heroGO);

		}


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
