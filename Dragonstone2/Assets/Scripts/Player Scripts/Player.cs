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

	public GameObject targetPointer;
	public GameObject targetPointerGO;

	Target target;

	void Awake () {

	}

	// Use this for initialization
	void Start () {



	}

	//public void InitHeroes ()
	//{
	//	StartCoroutine (InitHeroesRoutine());
	//}

	public IEnumerator InitHeroesRoutine ()
	{

		deck = GetComponent<Deck>();

		if (deck.heroes != null)
		{
			for (int i = 0; i < deck.heroes.Count; i++)
			{
				yield return new WaitForSeconds (0.2f);
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
				heroGO.GetComponentInChildren<OverheadText>().FloatingText(heroGO.name.ToString());
				


//add the abilities script to hero object
/*
				for (int j = 0; j < deck.heroes[i].abilityAsset.Count; j++)
				{
					string spellScriptName = deck.heroes[i].abilityAsset[j].abilityEffect;
					if (spellScriptName != null) {
						heroManager.abilityAssets.Add(deck.heroes[i].abilityAsset[j]);
					}
				}
*/

				for (int j = 0; j < deck.heroes[i].abilityAsset2.Count; j++)
				{
					string spellScriptName = deck.heroes[i].abilityAsset2[j].abilityEffect;
					if (spellScriptName != null) {
						heroManager.abilityAssets.Add(deck.heroes[i].abilityAsset2[j]);
						
					}

					
				}
				//heroManager.UpdateUI();
				//heroManager.CreateHeroPanel();

				//Set original reference values
				heroManager.origHealth = heroManager.maxHealth;
				heroManager.origAttack = heroManager.attack;
				heroManager.origDefense = heroManager.defense;
				heroManager.origChance = heroManager.chance;
				heroManager.origShield = heroManager.shield;


			}

		}

		yield return null;

	}//InitHeroes
	
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (dragging);

		if (isActive)
		{
			//if player is active, capture mouse pointer per frame
			GameObject pointerObject = UpdateMouseOver();		

			//if an object's collider is "hit"
			if (pointerObject != null )
			{
			
				//THIS IS FOR HERO ABILITY DRAGGING TO HERO
				//check that the object hit is a HERO.
				if(pointerObject.gameObject.GetComponent<HeroManager>() != null )
				{
					if (Input.GetMouseButtonDown(0) && pointerObject.gameObject.tag == gameObject.tag)
					{
						//if own hero is selected
						pointerObject.gameObject.GetComponent<HeroManager>().SelectHero();
						myHeroIsSelected = true;
					}

					if (Input.GetMouseButtonDown(0) && pointerObject.gameObject.tag != gameObject.tag)
					{
						//if enemy hero is selected
						pointerObject.gameObject.GetComponent<HeroManager>().DisplayHero();
					}
					
				}

				//if own Hero was selected, and pointer is now on an ability button
				if(pointerObject.gameObject.GetComponent<Button>() != null && myHeroIsSelected)
				{
					//if the button is clicked
					if (Input.GetMouseButtonDown(0))
					{
						button = pointerObject.gameObject.GetComponent<Button>();

						// //dragging is now active while the button is held down
						// dragging = true;

						//dragging pointer object is displayed
						targetPointerGO = Instantiate(targetPointer, button.gameObject.transform.position, button.gameObject.transform.rotation, button.gameObject.transform);

						//get the Ability script that corresponds to the button clicked
						target = pointerObject.gameObject.GetComponent<Button>().GetComponent<Ability>().target;

						//Passive and Inactive Implementation
						Ability target2 = pointerObject.gameObject.GetComponent<Button>().GetComponent<Ability>();
						if(target2.skillType == Type.Active){
							//dragging is now active while the button is held down
						dragging = true;

						}

					
						

						//Debug.Log(target.ToString());
						
					}
				}

				if (dragging)
				{
					
					//while dragging and mouse button is released
					if (Input.GetMouseButtonUp(0))
					{
					//ensure that the end of drag is pointing to a Hero
						if (target.ToString() == "Enemies" && pointerObject.gameObject.GetComponent<HeroManager>() != null && pointerObject.gameObject.GetComponent<HeroManager>().tag != tag)
						{
							//ebug.Log ("Attacking Enemy");
							ApplySkill(pointerObject);
						}

						else if (target.ToString() == "Allies" && pointerObject.gameObject.GetComponent<HeroManager>() != null && pointerObject.gameObject.GetComponent<HeroManager>().tag == tag)
						{
							//Debug.Log ("Attacking Ally");
							ApplySkill(pointerObject);
						}

						else if (target.ToString() == "Any" && pointerObject.gameObject.GetComponent<HeroManager>() != null)
						{
							//Debug.Log ("Attacking Any");
							ApplySkill(pointerObject);
						}

						else
						{
							//Debug.Log ("no target:");
							dragging = false;
							Destroy(targetPointerGO);

						}
					}

				}

				else
				{
					if (targetPointerGO != null)
					{
						Destroy(targetPointerGO);
					}
				}

			}

			else 
			{
				if (Input.GetMouseButtonUp(0))
				{
					dragging = false;
					if (targetPointerGO != null)
					{
						Destroy(targetPointerGO);
					}
				}
			}
			if (dragging)
			{
				if (pointerObject == null)
					{
						targetPointerGO.GetComponentInChildren<LineRenderer>().endColor = Color.blue;
						targetPointerGO.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
					}
				
	
				else if (target.ToString() == "Enemies")
				{
					if (pointerObject.gameObject.GetComponent<HeroManager>() != null && pointerObject.gameObject.GetComponent<HeroManager>().tag != tag)
					{
						
						targetPointerGO.GetComponentInChildren<LineRenderer>().endColor = Color.green;
						targetPointerGO.GetComponentInChildren<SpriteRenderer>().color = Color.green;
					}

					else
					{
						targetPointerGO.GetComponentInChildren<LineRenderer>().endColor = Color.red;
						targetPointerGO.GetComponentInChildren<SpriteRenderer>().color = Color.red;
					}
				}

				else if (target.ToString() == "Allies")
				{
					if (pointerObject.gameObject.GetComponent<HeroManager>() != null && pointerObject.gameObject.GetComponent<HeroManager>().tag == tag)
					{
						
						targetPointerGO.GetComponentInChildren<LineRenderer>().endColor = Color.green;
						targetPointerGO.GetComponentInChildren<SpriteRenderer>().color = Color.green;
					}

					else
					{
						targetPointerGO.GetComponentInChildren<LineRenderer>().endColor = Color.red;
						targetPointerGO.GetComponentInChildren<SpriteRenderer>().color = Color.red;
					}
				}

				else if (target.ToString() == "Any")
				{
					if (pointerObject.gameObject.GetComponent<HeroManager>() != null)
					{						
						targetPointerGO.GetComponentInChildren<LineRenderer>().endColor = Color.green;
						targetPointerGO.GetComponentInChildren<SpriteRenderer>().color = Color.green;
					}
				}
			}
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

	void ApplySkill(GameObject pointerObject)
	{
		if (button.GetComponent<Ability>().CanUseAbility())
		{
			button.GetComponent<Ability>().UseAbility(button.GetComponentInParent<HeroManager>(),pointerObject.gameObject.GetComponent<HeroManager>());

			//reset cooldown
			//button.GetComponent<Ability>().ResetCooldown();


			//floating text
			button.GetComponentInParent<HeroManager>().E_PopupMSG(button.GetComponent<Ability>().GetType().ToString());



		}		//let the gamemanager decide these:

		//GameManager.Instance.//CheckHealth ();
		//GameManager.Instance.//DeselectAllHeroes ();
		//GameManager.Instance.//NextTurn();
		//turn off dragging
		dragging = false;
		Destroy(targetPointerGO);
	}
}
