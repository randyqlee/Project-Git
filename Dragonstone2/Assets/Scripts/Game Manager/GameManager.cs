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

	//critical strike Implementation
	int attackersAttack, defendersAttack, atk_damage;

	//for Defender and Taunt Implementation - CheckDefenderAndTaunt
	public bool canTargetHero;

//for timer bar
	public float globalATB = 700;
	public float maxCharacterTurn = 10f;

	[SerializeField] Image timerBar;
	public float tempTimer;
	float delta = 0.1f;

	private IEnumerator ATBCoroutine;
//





	void Awake ()
	{

		Instance = this;
		//find all Players in the scene and store them in array

	}

	// Use this for initialization
	void Start () {

		StartCoroutine (GameLoop());



	}

	public void BattleTextMessage(string message)
	{
		//GetComponentInChildren<BattleTextController>().FloatingText(message);
	}


	IEnumerator GameLoop()
	{
		yield return StartCoroutine (InitPlayers());
		yield return new WaitForSeconds (2f);

		yield return StartCoroutine (InitHeroes());

		yield return new WaitForSeconds (2f);

		yield return StartCoroutine (InitHeroUI());

		yield return new WaitForSeconds (2f);
		

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
		BattleTextMessage("Initializing Heroes");

		 foreach (Player player in players)
		 {
		 	//player.GetComponent<Player>().InitHeroes();
			
			yield return StartCoroutine (player.GetComponent<Player>().InitHeroesRoutine());
		 }

		yield return null;
	}

	IEnumerator InitHeroUI()
	{
		BattleTextMessage("Initializing Hero UI");


		foreach (Player player in players)
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				hero.transform.Find("HeroUI").gameObject.transform.Find("Health").gameObject.SetActive(true);
				hero.transform.Find("HeroUI").gameObject.transform.Find("Attack").gameObject.SetActive(true);
				hero.transform.Find("HeroUI").gameObject.transform.Find("Defense").gameObject.SetActive(true);
				hero.UpdateUI();
				hero.CreateHeroPanel();
			}

		yield return null;
	}



	IEnumerator StartBattle()
	{

		BattleTextMessage("Start Battle!");
		yield return new WaitForSeconds (1f);

		foreach (HeroManager hero in players[0].GetComponentsInChildren<HeroManager>())
		{
			var image = hero.glow.GetComponent<Image>().color;
			image.a = 1f;
			hero.glow.GetComponent<Image>().color = image;
		}

		transform.Find("UI").gameObject.transform.Find("Turn Timer").gameObject.SetActive(true);

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

		
		//checks for Taunt
		if (IsTargetValid(attacker,defender))
		{
			CriticalStrikeCheck(attacker, defender);

			#region Old Attack Code
			// if (NoDefender(defender.GetComponentInParent<Player>()))
			// {
						
			// 	AttackStatusChecks(attacker, defender);

			// 	CheckHealth ();
			// 	DeselectAllHeroes ();
			// 	NextTurn();
			// }


			// else
			// {
			// 	if (defender.hasDefender)
			// 	{
			// 		AttackStatusChecks(attacker, defender);	
					
			// 		CheckHealth ();
			// 		DeselectAllHeroes ();
			// 		NextTurn();

			// 	}//has Defender

			// 	else
			// 		Debug.Log ("Attack Hero with defender only");
			// }
			#endregion

			//CheckTauntAndDefender Implementation

			CheckTauntAndDefender(attacker, defender);
			if(canTargetHero){
					AttackStatusChecks(attacker, defender);						
					CheckHealth ();
					DeselectAllHeroes ();
					NextTurn();

			}

		}//If IsTargetValid

	}//Attack Method


	//Defender check
	public bool NoDefender(Player player)
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
		

		foreach (Player player in GameManager.Instance.players )
		{
			if (player.tag == defender.gameObject.tag)
			{
				foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
				{
					#region old code
					// int atk_damage = attackersAttack - hero.defense;
					// //int def_damage = hero.attack - attacker.defense;

					// Debug.Log (attacker.gameObject.name + " is attacking: " + hero.gameObject.name + " for " + atk_damage + " hitpoints!");	

					// //hero.maxHealth = hero.maxHealth - atk_damage;

					// hero.TakeDamage(atk_damage, attacker);

					// //attacker.maxHealth = attacker.maxHealth - def_damage;

					// //display damage in UI

					// hero.DisplayDamageText(atk_damage);
					// //attacker.DisplayDamageText(def_damage);
					#endregion

					//set defender in current hero iteration to trigger reflect properly
					defender = hero;
					CriticalStrikeCheck(attacker, defender);
					AttackStatusChecks(attacker, defender);				
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

	bool IsGameOver()
	{
		foreach (Player player in players)
		{
			if (player.GetComponentsInChildren<HeroManager>().Length == 0)
			{
				
				if (player.tag == "Player 1")
				{
					BattleTextMessage ("Player 2 Wins!");
				}
				else
				{
					BattleTextMessage ("Player 1 Wins!");
				}
				return true;
			}
		}

		return false;
	}

	public void NextTurn ()
	{

		if (!IsGameOver())
		{
			if (ATBCoroutine != null)
			StopCoroutine (ATBCoroutine);
			//StopAllCoroutines();
			StartATBCoroutine ();
			//StartCoroutine (StartAttackSequence());

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

							BattleTextMessage ("Player 2");
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

							BattleTextMessage ("Player 1");
					}

				//trigger delegates for next turn - ex. decrease cooldown for abilities
				e_NextTurn();

				
				
				
			}
		}

		


		
	}

	IEnumerator StartAttackSequence()
	{

		
		tempTimer = maxCharacterTurn;
		while (tempTimer >0)
		{
			
			yield return new WaitForSeconds (delta);
			tempTimer -= delta;
			timerBar.fillAmount = tempTimer/maxCharacterTurn;
		}

		
		timerBar.fillAmount = 1;
		NextTurn ();
	}

	void StartATBCoroutine ()
	{
		ATBCoroutine = StartAttackSequence();
		StartCoroutine (ATBCoroutine);
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
			{
				debuff.duration = duration;
				debuff.gameObject.GetComponentInChildren<BuffPanel>().UpdateBuffIconCD(debuffName,duration);

			}
		}
		target.E_PopupMSG(debuffName);
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
			{
				buff.duration = duration;
				buff.gameObject.GetComponentInChildren<BuffPanel>().UpdateBuffIconCD(buffName,duration);
			}

		}

		target.E_PopupMSG(buffName);
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
				
	}//IsTargetValid

	public void DealDamage (int damage, HeroManager source, HeroManager target)
	{
		target.TakeDamage (damage, source);
	}

	//Checks for Critical Strike Flag and modifies attack damage
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

	}//Critical Strike Check

	//Checks and resolves resolution of Reflect, Echo, and Revenge
	public void AttackStatusChecks(HeroManager attacker, HeroManager defender){

		if (defender.hasReflect){

					atk_damage = attackersAttack-attacker.defense;
					attacker.TakeDamage (atk_damage, defender);
					Debug.Log("Damage Reflected");

					attacker.DisplayDamageText(atk_damage);
				} else {
					//Normal Damage	route
					atk_damage = attackersAttack - defender.defense;
					defender.TakeDamage(atk_damage, attacker);
					defender.DisplayDamageText(atk_damage);
				}

				//Revenge
				if (defender.hasRevenge)
				{
						atk_damage = defendersAttack-attacker.defense;
						attacker.TakeDamage (atk_damage, defender);
						Debug.Log("Revenge!");

						attacker.DisplayDamageText(atk_damage);
				}

				//Echo
				if(attacker.hasEcho)
				{
					atk_damage = attackersAttack-attacker.defense;
					attacker.TakeDamage (atk_damage, attacker);				
					Debug.Log("Echo Damage");

					attacker.DisplayDamageText(atk_damage);
				} 

	}

	public void CheckTauntAndDefender(HeroManager attacker, HeroManager defender){

		//For Defender
		//Check 3 states: 1) No Defender 2) Target has Defender 3) If you're target is an Ally
		if (NoDefender(defender.GetComponentInParent<Player>())|| defender.hasDefender|| defender.GetComponentInParent<Player>().tag == attacker.GetComponentInParent<Player>().tag ){

			canTargetHero = true;

		 } else {

			 canTargetHero = false;
			 Debug.Log ("Invalid Target: Attack Defender Only");
		 }

		//For Taunt

	}//Check Taunt and Defender
	

}//GameManager


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