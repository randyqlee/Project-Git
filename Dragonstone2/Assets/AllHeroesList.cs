using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHeroesList : MonoBehaviour {

	public List<GameObject> itemList;

	public HeroAsset[] allHeroAssets;

	public GameObject myCollection;

	public GameObject heroIcon;

	// Use this for initialization
	void Start () {




		
	}

	public void InitializeHeroPanel()
	{
		allHeroAssets = myCollection.GetComponent<MyCollection>().allHeroAssets;

		foreach (HeroAsset heroAsset in allHeroAssets)
		{
			
			GameObject heroGO = Instantiate (heroIcon); 
			heroGO.GetComponent<HeroIcon>().heroAsset = heroAsset;
			heroGO.transform.SetParent(gameObject.transform,false);
			itemList.Add(heroGO);

		}

	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
