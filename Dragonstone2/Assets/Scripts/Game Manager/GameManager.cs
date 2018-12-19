using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	// static
	// initialize 2 teams for battle
	// keep track of all happenings
	//initialize other systems

	public static GameManager Instance;

	//placeholder for Player gameobjects
	public Player[] players; 

	public delegate void Event_NextTurn();
	public event Event_NextTurn e_NextTurn = delegate {};


	public delegate void Event_HeroKilled();
	public event Event_HeroKilled e_HeroKilled = delegate {};

	public bool isInitialTurn = true;

	public bool isTurnPaused = false;

	int attackersAttack, defendersAttack;

	
	void Awake ()
	{

		Instance = this;
		//find all Players in the scene and store them in array

	}

	// Use this for initialization
	void Start () {

		StartCoroutine (GameLoop());



	}


	IEnumerator GameLoop()
	{
		yield return StartCoroutine (InitPlayers());

		yield return StartCoroutine (InitHeroes());

		yield return StartCoroutine (InitHeroUI());

		yield return StartCoroutine (StartBattle());

	}

	IEnumerator InitPlayers()
	{
		players = GameObject.FindObjectsOfType<Player>();

		players[1].isActive = true;
		players[0].isActive = false;



		yield return null;
	}

	IEnumerator InitHeroes()
	{
		 foreach (Player player in players)
		 	player.GetComponent<Player>().InitHeroes();

		yield return null;
	}

	IEnumerator InitHeroUI()
	{
		foreach (Player player in players)
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				hero.UpdateUI();
				hero.CreateHeroPanel();
			}

		yield return null;
	}

	IEnumerator StartBattle()
	{

		foreach (HeroManager hero in players[0].GetComponentsInChildren<HeroManager>())
		{
			var image = hero.glow.GetComponent<Image>().color;
			image.a = 1f;
			hero.glow.GetComponent<Image>().color = image;
		}

		NextTurn();


		yield return null;
	}

	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown ("a"))
		{
			CheckHealth ();
			DeselectAllHeroes ();
			NextTurn();
		}

	
	}



	public void Attack (HeroManager attacker, HeroManager defender)	{

		
		if (IsTargetValid(attacker,defender))
		{
			CriticalStrikeCheck(attacker, defender);

			if (NoDefender(defender.GetComponentInParent<Player>()))
			{
						
				

				int atk_damage = attackersAttack - defender.defense;
				//int def_damage = defender.attack - attacker.defense;							

				Debug.Log (attacker.gameObject.name + " is attacking: " + defender.gameObject.name + " for " + atk_damage + " hitpoints!");	

				//defender.maxHealth = defender.maxHealth - atk_damage;

				if (defender.hasReflect)
				{
					attacker.TakeDamage (attackersAttack-attacker.defense, defender);
				}

				if(attacker.hasEcho)
				{
					attacker.TakeDamage (attackersAttack-attacker.defense, attacker);
					defender.TakeDamage(atk_damage, attacker);
				}
				
				
				//normal damage route
				else 
				{
					defender.TakeDamage(atk_damage, attacker);
					
					if (defender.hasRevenge)
					{
						attacker.TakeDamage (defendersAttack-attacker.defense, defender);
					}
				}

				//attacker.maxHealth = attacker.maxHealth - def_damage;

				//display damage in UI

				defender.DisplayDamageText(atk_damage);
				//attacker.DisplayDamageText(def_damage);

				

				
				CheckHealth ();
				DeselectAllHeroes ();
				NextTurn();
			}


			else
			{
				if (defender.hasDefender)
				{
					int atk_damage = attackersAttack - defender.defense;
					//int def_damage = defender.attack - attacker.defense;

					Debug.Log (attacker.gameObject.name + " is attacking: " + defender.gameObject.name + " for " + atk_damage + " hitpoints!");	

					//defender.maxHealth = defender.maxHealth - atk_damage;

					//hasReflect
					if (defender.hasReflect)
					{
						//source is defender, but uses attacker's attack power
						attacker.TakeDamage (attackersAttack-attacker.defense, defender);
					}

					//hasEcho
					//if(attacker.hasEcho) {
						


					//}
					
					else 
					{
						defender.TakeDamage(atk_damage, attacker);
						if (defender.hasRevenge)
						{
							attacker.TakeDamage (defendersAttack-attacker.defense, defender);
						}
					}

					//attacker.maxHealth = attacker.maxHealth - def_damage;

					//display damage in UI

					defender.DisplayDamageText(atk_damage);
					//attacker.DisplayDamageText(def_damage);

					

					
					CheckHealth ();
					DeselectAllHeroes ();
					NextTurn();

				}//has Defender

				else
					Debug.Log ("Attack Hero with defender only");
			}
		}
	}//Attack Method


	//Defender check
	bool NoDefender(Player player)
	{
		foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
		{
			if (hero.hasDefender)
				return false;
		}
		return true;
	}

	public void AttackAll (HeroManager attacker, HeroManager defender)
	{
		CriticalStrikeCheck(attacker, defender);

		foreach (Player player in GameManager.Instance.players )
		{
			if (player.tag == defender.gameObject.tag)
			{
				foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
				{
					int atk_damage = attackersAttack - hero.defense;
					//int def_damage = hero.attack - attacker.defense;

					Debug.Log (attacker.gameObject.name + " is attacking: " + hero.gameObject.name + " for " + atk_damage + " hitpoints!");	

					//hero.maxHealth = hero.maxHealth - atk_damage;

					hero.TakeDamage(atk_damage, attacker);

					//attacker.maxHealth = attacker.maxHealth - def_damage;

					//display damage in UI

					hero.DisplayDamageText(atk_damage);
					//attacker.DisplayDamageText(def_damage);

					CheckHealth ();

				}
			}
		}

		

		DeselectAllHeroes ();
		NextTurn();
	}//AttackAll



	public void CheckHealth ()
	{
		foreach (Player player in players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				if (hero.maxHealth < 0)
					{
						KillHero(hero);
					}
				hero.UpdateUI();

			}
		}
	}

	public void KillHero (HeroManager hero)
	{
		Debug.Log ("Destroying " + hero.name);
		Destroy (hero.gameObject);
		e_HeroKilled();
	}

	public void DeselectAllHeroes ()
	{
		foreach (Player player in players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				if (hero.isSelected)
					{
						hero.DeselectHero();
					}
				

			}
		}
	}

	public void NextTurn ()
	{
		if (!isTurnPaused)
		{

			if (players[0].isActive)
					{
						players[0].isActive = false;
						players[1].isActive = true;

						foreach (HeroManager hero in players[0].GetComponentsInChildren<HeroManager>())
						{
							var image = hero.glow.GetComponent<Image>().color;
							image.a = 0f;
							hero.glow.GetComponent<Image>().color = image;


						}
						foreach (HeroManager hero in players[1].GetComponentsInChildren<HeroManager>())
						{
							var image = hero.glow.GetComponent<Image>().color;
							image.a = 1f;
							hero.glow.GetComponent<Image>().color = image;


						}
					}
				else {
						players[1].isActive = false;
						players[0].isActive = true;
						foreach (HeroManager hero in players[0].GetComponentsInChildren<HeroManager>())
						{
							var image = hero.glow.GetComponent<Image>().color;
							image.a = 1f;
							hero.glow.GetComponent<Image>().color = image;


						}
						foreach (HeroManager hero in players[1].GetComponentsInChildren<HeroManager>())
						{
							var image = hero.glow.GetComponent<Image>().color;
							image.a = 0f;
							hero.glow.GetComponent<Image>().color = image;


						}
				}

			//trigger delegates for next turn - ex. decrease cooldown for abilities
			e_NextTurn();
		}
		
	}



	public void AddDebuffComponent (string debuffName, int duration, HeroManager source, HeroManager target)
	{
		if (IsTargetValid(source, target))
		{
			if (target.hasImmunity)
			{
				Debug.Log ("Target Hero has immunity");
				//Immunity Implementation

			} else if (source.hasCrippledStrike){
				Debug.Log ("Source Hero has Crippled Strike");
				//Crippled Strike Implementation
			} else if (target.hasMalaise){
				//(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
				AddDebuff(debuffName, duration, source, target);

			} else if (IsChanceSuccess(source)){ //check for Chance
				//(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
				AddDebuff(debuffName, duration, source, target);
			}
		}
	}

	void AddDebuff (string debuffName, int duration, HeroManager source, HeroManager target)
	{
		if (!target.gameObject.GetComponent(System.Type.GetType(debuffName)))
		{
			(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
		}
		else
		{
			Debug.Log ("Debuff Already exists!");
			Debuff debuff = target.gameObject.GetComponent(System.Type.GetType(debuffName)) as Debuff;
			
			if (debuff.duration < duration)
				debuff.duration = duration;
		}
	}


	

/*
	void AddDebuff <T> (string debuffName, int duration, HeroManager source, HeroManager target) where T:Debuff
	{
		if (!target.gameObject.GetComponent(System.Type.GetType(debuffName)))
		{
			(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
		}
		else
		{
			Debug.Log ("Debuff Already exists!");
			
			if (target.gameObject.GetComponent<T>().duration < duration)
				target.gameObject.GetComponent<T>().duration = duration;
		}

	}
*/


	public void AddDebuffComponentRandom (string debuffName, int duration, HeroManager source, HeroManager target, int targetCount)
	{
		//HeroManager[] heroList = target.GetComponentInParent<Player>().GetComponentsInChildren<HeroManager>();
		List<HeroManager> heroList = new List<HeroManager>();
		heroList.AddRange(target.GetComponentInParent<Player>().GetComponentsInChildren<HeroManager>());
		List<HeroManager> randomHeroList = RandomHeroList (heroList);

		int count = 0;
		int heroCounter = 0;

		while (count < targetCount && heroCounter < randomHeroList.Count)
		{
			target = randomHeroList[heroCounter];

			if (target.hasImmunity)
			{
				Debug.Log ("Target Hero has immunity");
				//Immunity Implementation

			} else if (source.hasCrippledStrike){
				Debug.Log ("Source Hero has Crippled Strike");
				//Crippled Strike Implementation
			} else if (target.hasMalaise){
				//(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
				AddDebuff(debuffName, duration, source, target);
				count++;

			} else if (IsChanceSuccess(source)){ //check for Chance
				//(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
				AddDebuff(debuffName, duration, source, target);
				count++;
			}
			heroCounter++;
		}

	}

	List<HeroManager> RandomHeroList(List<HeroManager> heroList) {

		List<HeroManager> randomHeroList = new List<HeroManager>();
		randomHeroList.AddRange(heroList);

		for (int i = 0; i < randomHeroList.Count; i++) {
         HeroManager temp = randomHeroList[i];
         int randomIndex = Random.Range(i, randomHeroList.Count);
         randomHeroList[i] = randomHeroList[randomIndex];
         randomHeroList[randomIndex] = temp;
     }


		return randomHeroList;
	}

	public List<HeroManager> EnemyHeroList(HeroManager source) {

		List<HeroManager> enemyHeroList = new List<HeroManager>();
		foreach (Player player in GameManager.Instance.players )
		{
			if (player.tag != source.gameObject.tag)
			{
				foreach (HeroManager enemyHero in player.GetComponentsInChildren<HeroManager>())
				{
					enemyHeroList.Add(enemyHero);
				}
			}
		}
		return enemyHeroList;
	}

	public List<HeroManager> AllyHeroList(HeroManager source) {

		List<HeroManager> allyHeroList = new List<HeroManager>();
		foreach (Player player in GameManager.Instance.players )
		{
			if (player.tag == source.gameObject.tag)
			{
				foreach (HeroManager enemyHero in player.GetComponentsInChildren<HeroManager>())
				{
					allyHeroList.Add(enemyHero);
				}
			}
		}
		return allyHeroList;
	}


	public void AddBuffComponent (string buffName, int duration, HeroManager source, HeroManager target)
	{
		if(IsTargetValid(source, target))
		{
			if (target.hasImmunity)
			{
				Debug.Log ("Target Hero has immunity");
			}

			else if (IsChanceSuccess(source)) //check for Chance
			{
				//(target.gameObject.AddComponent(System.Type.GetType(buffName)) as Buff).New(duration,source.gameObject);
				AddBuff(buffName, duration, source, target);
			}
		}
	}

	void AddBuff (string buffName, int duration, HeroManager source, HeroManager target)
	{
		if (!target.gameObject.GetComponent(System.Type.GetType(buffName)))
		{
			(target.gameObject.AddComponent(System.Type.GetType(buffName)) as Buff).New(duration,source.gameObject);
		}
		else
		{
			Debug.Log ("Buff Already exists!");
			Buff buff = target.gameObject.GetComponent(System.Type.GetType(buffName)) as Buff;
			
			if (buff.duration < duration)
				buff.duration = duration;
		}
	}//AddBuff

	public void AddBuffComponentRandom (string buffName, int duration, HeroManager source, HeroManager target, int targetCount)
	{

		if (target.hasImmunity)
		{
			Debug.Log ("Target Hero has immunity");
		}

		else if (IsChanceSuccess(source)) //check for Chance
		{	//(target.gameObject.AddComponent(System.Type.GetType(buffName)) as Buff).New(duration,source.gameObject);
			AddBuff(buffName, duration, source, target);
		}

	}

	public void Heal (HeroManager source, HeroManager target)
	{
		if (!target.hasUnhealable)
		{

		}
	}


	bool IsChanceSuccess (HeroManager source)
	{
		float chance = source.chance;


		if ((1-Random.value) < chance/100)
			return true;
		else return false;
	}

	bool IsTargetValid(HeroManager attacker, HeroManager defender)
	{
		if (attacker.GetComponent<Taunt>() != null)
			if (attacker.GetComponent<Taunt>().source == defender)
				return true;
			else return false;
		else return true;
	}

	public void DealDamage (int damage, HeroManager source, HeroManager target)
	{
		target.TakeDamage (damage, source);
	}

	public void CriticalStrikeCheck(HeroManager attacker, HeroManager defender) {

		if(attacker.hasCritical){
					//50% chance for critical strike to be x2 or x3 damage
					if (1-Random.value < 0.5){
						attackersAttack = 2*attacker.attack;
					} else {
						attackersAttack = 3*attacker.attack;
					}

				} else {
					attackersAttack = attacker.attack;
				}

				if(defender.hasCritical){
					//50% chance for critical strike to be x2 or x3 damage
					if (1-Random.value < 0.5){
						defendersAttack = 2*defender.attack;
					} else {
						defendersAttack = 3*defender.attack;
					}
				} else {
					defendersAttack = defender.attack;
				}

	}
	

}


/*
	public void AddBuffComponent <T> (int duration, HeroManager source, HeroManager target) where T:Buff
	{


	}

*/
/*
		public void AddDebuffComponent <T> (int duration, HeroManager source, HeroManager target) where T:Debuff
	{

		if (target.hasImmunity)
		{
			Debug.Log ("Target Hero has immunity");
		}

		else
		target.gameObject.AddComponent<T>().New(duration,source.gameObject);

	}
*/