using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;

public class Player : MonoBehaviour {

	public Deck deck;
	public HeroManager heroPrefab;

	public GameObject spawnLocations;

	public bool isActive;

//	HeroManager selectedHero;

	public bool dragging = false;

	public GameObject heroPanel;
	
	HeroManager heroGO;
	bool myHeroIsSelected = false;

	Button button;



	void Awake () {
		isActive = false;
	}

	// Use this for initialization
	void Start () {

		deck = GetComponent<Deck>();

		if (deck.heroes != null)
		{
			for (int i = 0; i < deck.heroes.Count; i++)
			{
				Transform spawnLocation = spawnLocations.GetComponent<SpawnLocations>().spawn[i].transform;
				heroGO =  Instantiate(heroPrefab, spawnLocation.position, spawnLocation.rotation, transform);
				

				HeroManager heroManager = heroGO.GetComponent<HeroManager>();
				heroManager.heroName = deck.heroes[i].heroName;
				heroManager.image = deck.heroes[i].image;
				heroManager.maxHealth = deck.heroes[i].maxHealth;
				heroManager.attack = deck.heroes[i].attack;
				heroManager.defense = deck.heroes[i].defense;
				heroManager.chance = deck.heroes[i].chance;

				heroManager.rarity = deck.heroes[i].rarity;

				heroManager.player = this;
				heroManager.tag = this.tag;

				heroGO.GetComponentInChildren<Image>().sprite = deck.heroes[i].image;
				heroGO.name = heroManager.heroName;
				


//add the abilities script to hero object
				for (int j = 0; j < deck.heroes[i].abilityAsset.Count; j++)
				{
					string spellScriptName = deck.heroes[i].abilityAsset[j].abilityEffect;
					if (spellScriptName != null) {
/////					//ability = System.Activator.CreateInstance(System.Type.GetType(spellScriptName)) as Ability;
					
					//Type t = System.Type.GetType(spellScriptName);
					heroGO.gameObject.AddComponent(System.Type.GetType(spellScriptName));

					heroManager.abilityAssets.Add(deck.heroes[i].abilityAsset[j]);

					Ability[] abilityComponent = heroGO.gameObject.GetComponents<Ability>();

					Debug.Log ("Cooldown: " + deck.heroes[i].abilityAsset[j].abilityCoolDown);

					abilityComponent[j].abilityCooldown = deck.heroes[i].abilityAsset[j].abilityCoolDown;

					

					}
				}

				heroManager.abilities.AddRange(heroGO.GetComponentsInChildren<Ability>());

				heroManager.UpdateUI();
				heroManager.CreateHeroPanel();

				


			}

		}

	}
	
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (dragging);

		if (isActive)
		{
			GameObject pointerObject = UpdateMouseOver();
		

			if (pointerObject != null )
			{

				
			/*	THIS IS ONLY FOR HERO DRAGGING TO ANOTHER HERO
				if(pointerObject.gameObject.GetComponent<HeroManager>() != null && isActive)
				{



					if (Input.GetMouseButtonDown(0) && pointerObject.gameObject.tag == gameObject.tag && !dragging)
					{


						pointerObject.gameObject.GetComponent<HeroManager>().SelectHero();

						selectedHero = pointerObject.gameObject.GetComponent<HeroManager>();
						

						dragging = true;			


					}

					if (Input.GetMouseButtonDown(0) && pointerObject.gameObject.tag != gameObject.tag)
					{
						pointerObject.gameObject.GetComponent<HeroManager>().DisplayHero();
					}
				

					if (Input.GetMouseButtonUp(0) && selectedHero != null && pointerObject.gameObject.tag != gameObject.tag && dragging)
					{
						GameManager.Instance.Attack (selectedHero, pointerObject.gameObject.GetComponent<HeroManager>());
						dragging = false;
					}

					if (Input.GetMouseButtonUp(0))
					{
						dragging = false;
					}
				
				}
			*/

				//THIS IS FOR HERO ABILITY DRAGGING TO HERO
				//HERO is selected
				if(pointerObject.gameObject.GetComponent<HeroManager>() != null)
				{
					if (Input.GetMouseButtonDown(0) && pointerObject.gameObject.tag == gameObject.tag)
					{
						pointerObject.gameObject.GetComponent<HeroManager>().SelectHero();

						//selectedHero = pointerObject.gameObject.GetComponent<HeroManager>();

						myHeroIsSelected = true;
					}

					if (Input.GetMouseButtonDown(0) && pointerObject.gameObject.tag != gameObject.tag)
					{
						pointerObject.gameObject.GetComponent<HeroManager>().DisplayHero();
						myHeroIsSelected = false;
					}
					
				}

				//BUTTON is selected
				if(pointerObject.gameObject.GetComponent<Button>() != null && myHeroIsSelected)
				{
					if (Input.GetMouseButtonDown(0))
					{
						button = pointerObject.gameObject.GetComponent<Button>();

						dragging = true;
						
					}
				}

				if (dragging)
				{
					//ensure that the end of drag is pointing to a Hero
					if (Input.GetMouseButtonUp(0) && pointerObject.gameObject.GetComponent<HeroManager>() != null)
					{
						//replace with applying button's ability to target
						//GameManager.Instance.Attack (selectedHero, pointerObject.gameObject.GetComponent<HeroManager>());

						
						//check if ability is active and can be used

						if (button.GetComponent<Ability>().CanUseAbility())
						{
							button.GetComponent<Ability>().UseAbility(button.GetComponentInParent<HeroManager>(),pointerObject.gameObject.GetComponent<HeroManager>());

							//reset cooldown
							button.GetComponent<Ability>().ResetCooldown();



						}
						


						//let the gamemanager decide these:

						//GameManager.Instance.//CheckHealth ();
						//GameManager.Instance.//DeselectAllHeroes ();
						//GameManager.Instance.//NextTurn();
						//turn off dragging
						dragging = false;
					}

					//if (Input.GetMouseButtonUp(0))
					//{
					//	dragging = false;
					//}

				}

			}

			else 
				if (Input.GetMouseButtonUp(0))
					dragging = false;

		}
		
	}

	private GameObject UpdateMouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
			
			return hit.transform.gameObject;
          //pointer over hero  
		}
		else return null;

    }



}
