using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollection : MonoBehaviour {


	public GameObject allHeroesPanelContent;

	public HeroAsset[] allHeroAssets;

	public List<HeroAsset> playerDeckHeroAssets;

	public Animator transitionAnim;

	public GameObject mainMenu;



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
			GameObject.Find("Data Controller").gameObject.GetComponent<DataController>().SaveGameData(heroAsset);
		}

		//GameObject.Find("Data Controller").gameObject.GetComponent<DataController>().SaveGameData(playerDeckHeroAssets);

	}

	public void Back()
	{
		StartCoroutine (LoadMainMenu());

	}


	IEnumerator LoadMainMenu ()
	{
		transitionAnim.SetTrigger("start");
		yield return new WaitForSeconds (1.5f);
		//display mycollection screen
		
		mainMenu.SetActive(true);
		gameObject.SetActive(false);	



	}

}
