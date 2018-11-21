using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Deck deck;
	public HeroManager heroPrefab;

	public GameObject spawnLocations;

	public bool isActive;

	HeroManager selectedHero;

	public bool dragging = false;

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
				var heroGO =  Instantiate(heroPrefab, spawnLocation.position, spawnLocation.rotation, transform);
				

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
				heroManager.UpdateUI();



				string spellScriptName = deck.heroes[i].ability;
				if (spellScriptName != null) {
					//ability = System.Activator.CreateInstance(System.Type.GetType(spellScriptName)) as Ability;
					heroGO.gameObject.AddComponent(System.Type.GetType(spellScriptName));

				}

				


			}

		}

	}
	
	// Update is called once per frame
	void Update () {

		GameObject pointerObject = UpdateMouseOver();
	

		if (pointerObject != null)
		{
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
