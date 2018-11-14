using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float delayTime = 0.5f;

	//for setting up the game using prefabs
	public List<GameObject> prefab_team1;
	public List<GameObject> prefab_team2;

	public bool battleFinished = false;




	//placeholders for Instantiated prefabs
	public List<GameObject> team1 = new List<GameObject>();
	public List<GameObject> team2 = new List<GameObject>();
	public List<GameObject> all = new List<GameObject>();

	//Spawn locations
	public List<GameObject> location_team1;
	public List<GameObject> location_team2;

	public List<Hero> heroes;

	public float scale = 0.5f;


	void Awake () {

	/*	
		foreach (GameObject go in prefab_team1)
		{
			GameObject hero = Instantiate(go);	
			hero.tag = "Team1";
			team1.Add(hero);
		}
		foreach (GameObject go in prefab_team2)
		{
			GameObject hero = Instantiate(go);
			hero.tag = "Team2";
			team2.Add(hero);
		}
		foreach (GameObject go in team1)
			all.Add(go);
		foreach (GameObject go in team2)
			all.Add(go);

	*/

	}

	// Use this for initialization
	void Start () {

		StartCoroutine (GameLoop());
		
	}

	IEnumerator GameLoop()
	{
		yield return StartCoroutine (InitGameComponents());

		yield return StartCoroutine (HeroSpawnIntro());

		yield return StartCoroutine (LoadHeroUI());

		yield return StartCoroutine (LoadBattleUI());

		yield return StartCoroutine (UseBattlecry());

		yield return StartCoroutine (UseLeaderSkill());

		yield return StartCoroutine (StartBattle());

//		yield return StartCoroutine (EndBattle());



	}


	IEnumerator InitGameComponents()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Initializing Game Components");

		//gameObject.AddComponent<PlayerController>();
		//gameObject.AddComponent<GlobalATB>();
		yield return new WaitForSeconds (delayTime);

	}

	IEnumerator HeroSpawnIntro()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Hero Spawn Intro");

		for (int i=0; i<prefab_team1.Count; i++)
		{
			GameObject hero = Instantiate(prefab_team1[i],location_team1[i].transform.position, location_team1[i].transform.rotation);	
			hero.tag = "Team1";
			team1.Add(hero);
			yield return new WaitForSeconds (delayTime);

			hero = Instantiate(prefab_team2[i], location_team2[i].transform.position, location_team2[i].transform.rotation);	
			hero.tag = "Team2";
			team2.Add(hero);
			yield return new WaitForSeconds (delayTime);
			
		}

		foreach (GameObject go in team1)
			all.Add(go);
		foreach (GameObject go in team2)
			all.Add(go);



		gameObject.AddComponent<GlobalEventManager>();




		yield return new WaitForSeconds (delayTime);

	}


	IEnumerator LoadHeroUI()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Loading Hero UI");

		foreach (GameObject go in all)
			go.transform.Find("UI").gameObject.SetActive(true);

	

		yield return new WaitForSeconds (delayTime);

	}
	IEnumerator LoadBattleUI()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Loading Battle UI");
		yield return new WaitForSeconds (delayTime);

	}
	IEnumerator UseBattlecry()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Battlecry!!!");

		heroes = new List<Hero> (FindObjectsOfType<Hero>());	

		BubbleSort (heroes);

		foreach (Hero hero in heroes)
		{
			//hero.GetComponentInParent<SkillSystem>().skillComponents[3].UseBattlecry();

			Debug.Log (hero.name.ToString());


			if (hero.tagHero != null)
			{

				hero.skills[3] = hero.tagHero.GetComponent<Hero>().skills[3];

				hero.GetComponent<SkillSystem>().ReplaceSkill(3, hero.tagHero.GetComponent<Hero>().skills[3]);


				hero.currAttribs.baseHP += scale * hero.tagHero.GetComponent<Hero>().attributes.baseHP;
				hero.currAttribs.baseDamage += scale* hero.tagHero.GetComponent<Hero>().attributes.baseDamage;
				hero.currAttribs.baseArmor += scale * hero.tagHero.GetComponent<Hero>().attributes.baseArmor;
				hero.currAttribs.baseSpeed += scale * hero.tagHero.GetComponent<Hero>().attributes.baseSpeed;


				hero.GetComponent<SkillSystem>().skillComponents[3].isActive=true;
				hero.GetComponent<SkillSystem>().skillComponents[3].UseBattlecry();


			}


//			hero.GetComponent<SkillSystem>().skillComponents[3].UseBattlecry();


			yield return new WaitForSeconds (delayTime);
		}

		yield return new WaitForSeconds (delayTime);

	}
	IEnumerator UseLeaderSkill()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Activating Leader skills");


		
		yield return new WaitForSeconds (delayTime);

	}
	IEnumerator StartBattle()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Start Battle");


		transform.Find("UI").gameObject.transform.Find("Turn Timer").gameObject.SetActive(true);

		GetComponent<GlobalATB>().enabled = true;
		GetComponent<PlayerController>().enabled = true;
		yield return new WaitForSeconds (delayTime);

	}


	IEnumerator EndBattle()
	{
		GetComponentInChildren<BattleTextController>().FloatingText("Game Over");
		yield return new WaitForSeconds (delayTime);

		GetComponent<GlobalATB>().enabled = false;
		GetComponent<PlayerController>().enabled = false;

	}

	public IEnumerator Winner (string team)
	{
		battleFinished = true;
		if (team == "Team1")
			GetComponentInChildren<BattleTextController>().FloatingText("Team 1 Wins the battle");
		if (team == "Team2")
			GetComponentInChildren<BattleTextController>().FloatingText("Team 2 Wins the battle");
		
		yield return new WaitForSeconds (2*delayTime);

		StartCoroutine (EndBattle());

	}



//optimize this! There is a better way!
	void BubbleSort (List<Hero> activeHeroes)
	{
		if (activeHeroes.Count > 1)
		{
			for (int j = 0; j<activeHeroes.Count-1; j++)
			{
				for (int i=0; i<activeHeroes.Count-1; i++)
				{
					if (activeHeroes[i+1].GetComponentInParent<ATBTimer>().turn > activeHeroes[i].GetComponentInParent<ATBTimer>().turn)
					{
						Swap (activeHeroes, i);
					}
					else if (activeHeroes[i+1].GetComponentInParent<ATBTimer>().turn == activeHeroes[i].GetComponentInParent<ATBTimer>().turn)
					{
						if (activeHeroes[i+1].currAttribs.baseSpeed > activeHeroes[i].currAttribs.baseSpeed)
						{
							Swap (activeHeroes, i);
						}
						else if (activeHeroes[i+1].currAttribs.baseSpeed == activeHeroes[i].currAttribs.baseSpeed)
						{
							int rand = Random.Range (0, 2);
							if (rand == 1)
							{
								Swap (activeHeroes, i);
							}

						}
					}
				}
			}
		}
	}

	void Swap (List<Hero> activeHeroes, int i)
	{
		Hero c = activeHeroes[i];
		activeHeroes[i] = activeHeroes[i+1];
		activeHeroes[i+1] = c;

	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
