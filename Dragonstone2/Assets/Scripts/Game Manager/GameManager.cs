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

	// public delegate void Event_NextTurn();
	// public event Event_NextTurn e_NextTurn = delegate {};	

	public delegate void Event_HeroKilled();
	public event Event_HeroKilled e_HeroKilled = delegate {};	

	public delegate void Event_PlayerStartPhase();
	public event Event_PlayerStartPhase e_PlayerStartPhase = delegate {};

	public delegate void Event_PlayerMainPhase();
	public event Event_PlayerMainPhase e_PlayerMainPhase = delegate {};

	public delegate void Event_PlayerEndPhase();
	public event Event_PlayerEndPhase e_PlayerEndPhase = delegate {};	

	public delegate void Event_CriticalStrike();
	public event Event_CriticalStrike e_CriticalStrike = delegate {};

	public delegate void Event_DealDamage(int damage, HeroManager target);
	public event Event_DealDamage e_DealDamage = delegate {};

	public bool isInitialTurn = true;

	public bool isTurnPaused = false;
	

	//critical strike Implementation
	public int attackersAttack, defendersAttack, atk_damage;

	//for Defender and Taunt Implementation - CheckDefenderAndTaunt
	public bool canTargetHero, checkDefender;
	public bool extraTurn;

	int heroTurnP1 = 0;
	int heroTurnP2 = 0;
	int player1TeamSize;
	int player2TeamSize;

	//Variables used in extra turn
	// [HideInInspector]
	// public List<HeroManager> extraTurnHeroes;

	
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
		GetComponentInChildren<BattleTextController>().FloatingText(message);
	}


	IEnumerator GameLoop()
	{
		yield return StartCoroutine (InitPlayers());
		//yield return new WaitForSeconds (2f);
		yield return StartCoroutine (InitHeroes());
		//yield return new WaitForSeconds (2f);
		yield return StartCoroutine (InitHeroUI());
		//yield return new WaitForSeconds (2f);
		yield return StartCoroutine (InitHeroPassives());
		//yield return new WaitForSeconds (2f);
		yield return StartCoroutine (StartBattle1());
		//yield return new WaitForSeconds (2f);
		
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

				//Add reference to the heroes
				
				player.teamHeroes.Add(hero.gameObject);		
				
			}



		yield return null;
	}//InitHeroUI



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

		//NextTurn();
		StartCoroutine(PlayerTurnEnd());
		yield return null;
	}//Start Battle

	IEnumerator StartBattle1()
	{
			
		BattleTextMessage("Start Battle!");
		yield return new WaitForSeconds (1f);

		//initial turn
		HeroManager firstPlayer = players[0].teamHeroes[heroTurnP1].GetComponent<HeroManager>();
		firstPlayer.SelectHero();
		var image = firstPlayer.glow.GetComponent<Image>().color;
		image.a = 1f;
		firstPlayer.glow.GetComponent<Image>().color = image;		

		transform.Find("UI").gameObject.transform.Find("Turn Timer").gameObject.SetActive(true);

		player1TeamSize = players[0].teamHeroes.Count;
		player2TeamSize = players[1].teamHeroes.Count;

		//heroTurnP1 ++;

		//NextTurn();
		StartCoroutine(PlayerTurnEnd());
		yield return null;
	}//Start Battle

	IEnumerator InitHeroPassives()
	{
		BattleTextMessage("Initializing Hero Passives");
		//Debug.Log("Initializing Hero Passives");


		foreach (Player player in players)
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				
				// hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				// Ability[] abilities = hero.GetComponentsInChildren<Ability>();
				// foreach(Ability ability in abilities){
				// 	if(ability.skillType == Type.Passive){
				// 		ability.UseAbilityPassive();
				// 	}

				// 	//For active skills with Passive
				// 	if(ability.skillType == Type.Active){
				// 		ability.UseAbilityActive();
				// 	}

				// 	// ability.UseAbilityPassive();
				// 	// ability.UseAbilityActive();
				// }

				// hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);

				StartCoroutine(EnableHeroPassives(hero));
			}

		yield return null;
	}//InitHeroUI

	public IEnumerator EnableHeroPassives(HeroManager hero){
			hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				Ability[] abilities = hero.GetComponentsInChildren<Ability>();
				foreach(Ability ability in abilities){
					if(ability.skillType == Type.Passive){
						ability.UseAbilityPassive();
					}

					//For active skills with Passive
					if(ability.skillType == Type.Active){
						ability.UseAbilityActive();
					}

					//  ability.UseAbilityPassive();
					//  ability.UseAbilityActive();
				}
				
				hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);

		yield return null;
	}

	
	// Update is called once per frame
	void Update () {
		//just for debugging, to simulate NextTurn
		EndPlayerTurn();		
	}

	public void EndPlayerTurn(){

		if (Input.GetKeyDown ("a"))
		{
			EndButton();
		}	

	}

	public void EndButton(){
		//GameManager.Instance.DeselectAllHeroes();
		StartCoroutine(PlayerTurnEnd());
	}

	public void Attack (HeroManager attacker, HeroManager defender)	{

		
			//CriticalStrikeCheck(attacker, defender);		
					
			//Checks for Taunt and Defender
			CheckTaunt(attacker, defender);


			if(canTargetHero){
					AttackStatusChecks(attacker, defender);						
					
					//transferred to base.UseAbility()
					//EndTurn();
			}	

	}//Attack Method


	//Defender check
	public bool NoThreat(Player player)
	{
		foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
		{
			if (hero.hasThreat)
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
					//set defender in current hero iteration to trigger reflect properly
					defender = hero;
					//CriticalStrikeCheck(attacker, defender);
					AttackStatusChecks(attacker, defender);				
							

				}
				
			}
		}
		
		//transferred to base.UseAbility
		//EndTurn();
	}//AttackAll



	public void CheckHealth ()
	{
		foreach (Player player in players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				if (hero.maxHealth < 0)
				{
					if(hero.hasEndure)
					{
						hero.maxHealth = 1;
						BattleTextMessage ("Endure: " +hero.name);
					} 
					else 
					{
						hero.isDead = true;
						KillHero(hero);
					}

				}	
				hero.UpdateUI();
			}
		}
	}

	// //Original Code
	// public void KillHero (HeroManager hero)
	// {		
	// 	Debug.Log ("Destroying " + hero.name);
	// 	Destroy (hero.gameObject);
	// 	e_HeroKilled();		
	// }

	//Original Code
	public void KillHero (HeroManager hero)
	{		
		// Debug.Log("Kill Hero: " +hero);
		// hero.isDead = true;
				
		// Buff[] buffs = hero.GetComponents<Buff>();
		
		// 	foreach(Buff buff in buffs){					
		// 		Destroy(buff);				
		// 	}

		// //Destroy all debuffs
		// Debuff[] debuffs = hero.GetComponents<Debuff>();
		
		// 	foreach(Debuff debuff in debuffs){					
		// 		Destroy(debuff);					
		// 	}		
		
		
		
		// //Disable Passive Abilities		
		// hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);	
		// Debug.Log("Hero Panel Access: " +hero.transform.Find("HeroPanel(Clone)").gameObject);						
		// Ability[] abilities = hero.GetComponentsInChildren<Ability>();
		
		// 	foreach(Ability ability in abilities){
		// 			if(ability.skillType == Type.Passive){
		// 				ability.DisableAbilityPassive();
		// 				Debug.Log("Passive Ability Disabled: " +ability);
		// 			}

		// 			if(ability.skillType == Type.Active){
		// 				ability.DisableAbilityActive();
		// 				Debug.Log("Active Ability Disabled: " +ability);
		// 			}

					
		// 		}
		// hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);

		
		
		// //hero.gameObject.transform.GetInstanceID
		// //Kill Hero

		// hero.GetComponentInParent<Player>().DeadHeroes(hero);


		// hero.gameObject.SetActive(false);

		// e_HeroKilled();	

		// if(hero.hasProtectSoul){
		// 	StartCoroutine(ProtectSoulRevive(hero));
		// }		
		StartCoroutine(KillHeroCoroutine(hero));

	}//killhero

	public IEnumerator KillHeroCoroutine (HeroManager hero)
	{		
					
		//yield return StartCoroutine(DestroyBuffsDebuffs(hero));
		
		//Disable Passive Abilities		
		//yield return StartCoroutine(DisableHeroPassives(hero));

		
		
		//hero.gameObject.transform.GetInstanceID
		//Kill Hero
		yield return StartCoroutine(KillHeroEvent(hero));		

		if(hero.hasProtectSoul){
			yield return StartCoroutine(ProtectSoulRevive(hero));
		}		

	}//killhero

	public IEnumerator DestroyBuffsDebuffs(HeroManager hero){

		Buff[] buffs = hero.GetComponents<Buff>();
		
			foreach(Buff buff in buffs){					
				Destroy(buff);				
			}

		//Destroy all debuffs
		Debuff[] debuffs = hero.GetComponents<Debuff>();
		
			foreach(Debuff debuff in debuffs){					
				Destroy(debuff);					
			}		
		
		yield return null;
	}//DestroyBuffsDebuffs

	public IEnumerator DisableHeroPassives(HeroManager hero){
		//Disable Passive Abilities		
		hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);	
		
		Ability[] abilities = hero.GetComponentsInChildren<Ability>();
		
			foreach(Ability ability in abilities){
					
					ability.DisableAbilityPassive();
					ability.DisableAbilityActive();
					
				}
		hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);
		yield return null;
	}//Disable Passives

	public IEnumerator KillHeroEvent(HeroManager hero){
		hero.isDead = true;
		hero.GetComponentInParent<Player>().DeadHeroes(hero);
		hero.gameObject.SetActive(false);
		e_HeroKilled();	
		yield return null;
	}

	public  IEnumerator ProtectSoulRevive(HeroManager hero){
			hero.gameObject.SetActive(true);
			ReinitializeHero(hero);
			Debug.Log("Protect Soul");

		yield return null;
	}

	//used for revive skills
	public void ReinitializeHero(HeroManager hero) {
		
		
		//Destroy all buffs
		Buff[] buffs = hero.GetComponents<Buff>();
		
			foreach(Buff buff in buffs){					
				Destroy(buff);				
			}

		//Destroy all debuffs
		Debuff[] debuffs = hero.GetComponents<Debuff>();
		
			foreach(Debuff debuff in debuffs){					
				Destroy(debuff);					
			}
		//reinitialize stats
		hero.maxHealth = hero.origHealth;
		hero.attack = hero.origAttack;
		hero.defense = hero.origDefense;
		hero.chance = hero.origChance;
		hero.shield = hero.origShield;

		//Trigger Passives
		hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				Ability[] abilities = hero.GetComponentsInChildren<Ability>();
				foreach(Ability ability in abilities){
					if(ability.skillType == Type.Passive){
						ability.UseAbilityPassive();
					}

					//For active skills with Passive
					if(ability.skillType == Type.Active){
						ability.UseAbilityActive();
					}
				}

		hero.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);

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
					
				if (!hero.isSelected)
					{
						hero.DeselectHeroPanel();
					}

			}
		}
	}


	public void DeselectAllHeroPanels ()
	{
		foreach (Player player in players)
		{
			foreach (HeroManager hero in player.GetComponentsInChildren<HeroManager>())
			{
				
						hero.DeselectHeroPanel();
				
				

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

	// public void  NextTurn ()
	// {		
	// 	if (!IsGameOver())
	// 	{			
	// 		if (ATBCoroutine != null)
	// 		StopCoroutine (ATBCoroutine);
	// 		StartATBCoroutine ();

	// 		if (!isTurnPaused)
	// 		{
	// 			SwitchActivePlayers();				
	// 			//trigger delegates for next turn - ex. decrease cooldown for abilities
	// 			e_NextTurn();							
	// 		}
	// 	}		
	// }//Next Turn

	IEnumerator PlayerTurnStart(){
		
		
		
		
		StopCoroutine(PlayerTurnEnd());

		yield return StartCoroutine(PlayerStartPhase());

		yield return StartCoroutine(PlayerMainPhase());

		//yield return StartCoroutine(PlayerEndPhase());

		yield return null;

	}

	IEnumerator PlayerTurnEnd(){
		
		StopCoroutine(PlayerTurnStart());
		
		yield return StartCoroutine(PlayerEndPhase());

		yield return StartCoroutine(PlayerTransition());

		yield return StartCoroutine(PlayerTurnStart());

	}

	IEnumerator PlayerStartPhase(){		
		
		e_PlayerStartPhase();
		//Debug.Log("Player Start Phase");
		yield return null;
		
	}

	IEnumerator PlayerMainPhase(){
		
		e_PlayerMainPhase();
		//Debug.Log("Player Main Phase");
		
		//Start Timer
		if (!IsGameOver())
		{			
			if (ATBCoroutine != null)
			StopCoroutine (ATBCoroutine);
			StartATBCoroutine ();
		}
	
		yield return null;	
	}

	IEnumerator PlayerEndPhase(){
		e_PlayerEndPhase();
		//Debug.Log("Player End Phase");

		yield return null;
		
	}

	IEnumerator PlayerTransition(){

		if (!isTurnPaused)
			{
				//SwitchActivePlayers();
				SwitchActivePlayers1();				
				
			}		
		//Debug.Log("Player Transition");
		yield return null;
		
	}
	


	void SwitchActivePlayers(){
		if (players[0].isActive)
						{
							players[0].isActive = false;
							players[1].isActive = true;
							players[1].isEndTurn = false;

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
							players[0].isEndTurn = false;
							
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


	}//SwitchActivePlayers


	void SwitchActivePlayers1(){
		if (players[0].isActive)
						{
							players[0].isActive = false;
							players[1].isActive = true;
							players[1].isEndTurn = false;

							
							GameManager.Instance.DeselectAllHeroes();

							//Set hero.isActive to flase						


							if(!IsGameOver()){
								while(!players[1].teamHeroes[heroTurnP2].activeSelf){							
									heroTurnP2++;
									if(heroTurnP2>=player2TeamSize)
									heroTurnP2 = 0;
								} 
							}

							players[1].teamHeroes[heroTurnP2].GetComponent<HeroManager>().SelectHero();
							var image = players[1].teamHeroes[heroTurnP2].GetComponent<HeroManager>().glow.GetComponent<Image>().color;
							image.a = 1f;
							players[1].teamHeroes[heroTurnP2].GetComponent<HeroManager>().glow.GetComponent<Image>().color = image;					
								

							//Disable Hero Selector
							foreach (HeroManager hero in players[0].GetComponentsInChildren<HeroManager>())
							{
								var image0 = hero.glow.GetComponent<Image>().color;
								image0.a = 0f;
								hero.glow.GetComponent<Image>().color = image0;
							}
							
							heroTurnP2++;
							if(heroTurnP2>=player2TeamSize)
							heroTurnP2 = 0;
														
							
							BattleTextMessage ("Player 2");
						}
					else {
							players[1].isActive = false;
							players[0].isActive = true;
							players[0].isEndTurn = false;
							
							
							//Set Hero Active
							GameManager.Instance.DeselectAllHeroes();

							if(!IsGameOver()){
								while(!players[0].teamHeroes[heroTurnP1].activeSelf){							
									heroTurnP1++;
									if(heroTurnP1>=player2TeamSize)
									heroTurnP1 = 0;
								} 
							}



							players[0].teamHeroes[heroTurnP1].GetComponent<HeroManager>().SelectHero();

							var image = players[0].teamHeroes[heroTurnP1].GetComponent<HeroManager>().glow.GetComponent<Image>().color;
							image.a = 1f;
							players[0].teamHeroes[heroTurnP1].GetComponent<HeroManager>().glow.GetComponent<Image>().color = image;



							foreach (HeroManager hero in players[1].GetComponentsInChildren<HeroManager>())
							{
								var image1 = hero.glow.GetComponent<Image>().color;
								image1.a = 0f;
								hero.glow.GetComponent<Image>().color = image1;			

							}

							heroTurnP1++;
							if(heroTurnP1>=player1TeamSize)
							heroTurnP1 = 0;

							BattleTextMessage ("Player 1");
					}


	}//SwitchActivePlayers

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

		//NextTurn ();
		StartCoroutine(PlayerTurnEnd());
	}

	void StartATBCoroutine ()
	{
		ATBCoroutine = StartAttackSequence();
		StartCoroutine (ATBCoroutine);
	}

	public void AddDebuffComponent (string debuffName, int duration, HeroManager source, HeroManager target)
	{
		if (canTargetHero)
		{
			if (target.hasImmunity || target.hasPermanentImmunity)
			{
				Debug.Log ("Target Hero has immunity");
				//Immunity Implementation

			} else if (source.hasCrippledStrike){
				Debug.Log ("Source Hero has Crippled Strike");
				//Crippled Strike Implementation
			} else if (source.hasSilence){
				Debug.Log ("Source Hero has Silence");
				//Crippled Strike Implementation
			}else if (target.hasMalaise){
				//(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
				AddDebuff(debuffName, duration, source, target);

			} else if (IsChanceSuccess(source)){ //check for Chance
				//(target.gameObject.AddComponent(System.Type.GetType(debuffName)) as Debuff).New(duration,source.gameObject);
				AddDebuff(debuffName, duration, source, target);
			}
		}//IsTargetValid
	}

	public void AddDebuff (string debuffName, int duration, HeroManager source, HeroManager target)
	{
		//Debug.Log("Debuff Name: " +debuffName);
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


	
//Another implementation of AddDebuff, with generic Type as input
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
		List<HeroManager> heroList = new List<HeroManager>();
		heroList.AddRange(target.GetComponentInParent<Player>().GetComponentsInChildren<HeroManager>());
		List<HeroManager> randomHeroList = RandomHeroList (heroList);

		int count = 0;
		int heroCounter = 0;

		while (count < targetCount && heroCounter < randomHeroList.Count) 
		{
			target = randomHeroList[heroCounter];

			if (target.hasImmunity || target.hasPermanentImmunity)
			{
				Debug.Log ("Target Hero has immunity");
				//Immunity Implementation

			} else if (source.hasCrippledStrike){
				Debug.Log ("Source Hero has Crippled Strike");
				//Crippled Strike Implementation
			} else if (source.hasSilence){
				Debug.Log ("Source Hero has Silence");
				//Crippled Strike Implementation
			}else if (target.hasMalaise){
				AddDebuff(debuffName, duration, source, target);
				count++;

			} else if (IsChanceSuccess(source)){ //check for Chance
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
	}//EnemyHero List

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

		if(canTargetHero)
		{
			if (target.hasAntiBuff)
			{
				Debug.Log ("Target Hero has AntiBuff");
				//AddBuff(buffName, duration, source, target);
			} else if (source.hasSilence){
				Debug.Log ("Source Hero has Silence");
				//Crippled Strike Implementation
			}

			else if (IsChanceSuccess(source)) //check for Chance
			{
				AddBuff(buffName, duration, source, target);
			}
		}//IsTargetValid

		
	}

	public void AddBuff (string buffName, int duration, HeroManager source, HeroManager target)
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
		//Debug.Log("Add Buff" +buffName);

	}//AddBuff
	

	public void AddBuffComponentRandom (string buffName, int duration, HeroManager source, HeroManager target, int targetCount)
	{
		List<HeroManager> heroList = new List<HeroManager>();
		heroList.AddRange(target.GetComponentInParent<Player>().GetComponentsInChildren<HeroManager>());
		List<HeroManager> randomHeroList = RandomHeroList (heroList);

		int count = 0;
		int heroCounter = 0;

		while (count < targetCount && heroCounter < randomHeroList.Count) 
		{
			target = randomHeroList[heroCounter];	

			if (target.hasAntiBuff)
			{
				Debug.Log ("Target Hero has AntiBuff");
				//AddBuff(buffName, duration, source, target);
			} else if (source.hasSilence){
				Debug.Log ("Source Hero has Silence");
				//Crippled Strike Implementation
			}

			else if (IsChanceSuccess(source)){ //check for Chance
				AddBuff(buffName, duration, source, target);
				count++;
			}
			heroCounter++;
		}
		
	}

	public void Heal (HeroManager target, int healValue)
	{
		target.Healhero(target, healValue);
	}//Heal


	public bool IsChanceSuccess (HeroManager source)
	{
		float chance = source.chance;


		if ((1-Random.value) < chance/100)
			return true;
		else return false;
	}

	// bool IsTargetValid(HeroManager attacker, HeroManager defender)
	// {
	// 	if (attacker.GetComponent<Taunt>() != null)
	// 		if (attacker.GetComponent<Taunt>().source == defender)
	// 			return true;
	// 		else return false;		 
	// 	else return true;
				
	// }//IsTargetValid

	public void DealDamage (int damage, HeroManager source, HeroManager target)	{
		
		//StartCoroutine(DealDamageCoroutine(damage, source, target));		
		DealDamage1(damage, source, target);
		
	}//Deal Damage

	public IEnumerator DealDamageCoroutine (int damage, HeroManager source, HeroManager target)
	{
		if(target.hasBrand){

			DebuffAsset debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Brand");
			int brandDamage = debuff.value;
			damage += brandDamage;
			
			//Enumerator
			if(damage >= target.maxHealth){				
				//e_DealDamage(damage, target);
				yield return StartCoroutine(DealDamageEvent(damage, target));
			}

			//Enumerator
			//target.TakeDamage (damage, source);		
			yield return StartCoroutine(TakeDamageCoroutine(damage, source, target));			
			

		}else{
			
			if(damage >= target.maxHealth){
				//e_DealDamage(damage, target);
				yield return StartCoroutine(DealDamageEvent(damage, target));
			}

			//target.TakeDamage (damage, source);
			yield return StartCoroutine(TakeDamageCoroutine(damage, source, target));
			
		}

		yield return null;		
		
	}//Deal Damage1

	public void DealDamage1 (int damage, HeroManager source, HeroManager target)
	{
		if(target.hasBrand){

			DebuffAsset debuff = Resources.Load<DebuffAsset>("SO Assets/Debuff/Brand");
			int brandDamage = debuff.value;
			damage += brandDamage;
			
			//Enumerator
			if(damage >= target.maxHealth){				
				//e_DealDamage(damage, target);
				e_DealDamage(damage, target);	
			}

			//Enumerator
			//target.TakeDamage (damage, source);		
			target.TakeDamage(damage, source);		
			

		}else{
			
			if(damage >= target.maxHealth){
				//e_DealDamage(damage, target);
				e_DealDamage(damage, target);	
			}

			//target.TakeDamage (damage, source);
			target.TakeDamage(damage, source);
			
		}			
		
	}//Deal Damage1


	public IEnumerator DealDamageEvent(int damage, HeroManager target){

		e_DealDamage(damage, target);	
		yield return null;
	}

	public IEnumerator TakeDamageCoroutine(int damage, HeroManager source, HeroManager target){

		StartCoroutine(target.TakeDamageCoroutine(damage, source));
		yield return null;
	}	

	

	public void AttackCritical(HeroManager attacker, HeroManager defender){
		bool critStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		Attack(attacker, defender);
		attacker.hasCritical = critStatus;

	}

	public void AttackAllCritical(HeroManager attacker, HeroManager defender){
		bool critStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		AttackAll(attacker, defender);
		attacker.hasCritical = critStatus;

	}

	//Checks and resolves resolution of Reflect, Echo, and Revenge
	public void AttackStatusChecks(HeroManager attacker, HeroManager defender){

		int attackersDefense = attacker.defense;
		int defendersDefense = defender.defense;
		attackersAttack = attacker.attack;
		defendersAttack = defender.attack;
		
		if(attackersDefense < 0)
			attackersDefense = 0;

		if(defendersDefense < 0)		
			defendersDefense = 0;

		if(attackersAttack < 0)
			attackersAttack = 0;

		if(defendersAttack<0)		
			defendersAttack = 0;	

		CriticalStrikeCheck(attacker, defender);

		if (defender.hasReflect){									
					
			if(!defender.GetComponentInParent<Player>().isActive)
			{
				atk_damage = attackersAttack-attackersDefense;
				//attacker.TakeDamage (atk_damage, defender);
				DealDamage(atk_damage, defender, attacker);
				Debug.Log("Damage Reflected");
				attacker.DisplayDamageText(atk_damage);
			}					
					
		} else {
					
					if(defender.hasDefend){

						HeroManager defenderHero = defender.GetComponent<Defend>().source.GetComponent<HeroManager>();						
						int defenderHerosDefense = defenderHero.defense;

						atk_damage = attackersAttack - defenderHerosDefense;					
						DealDamage(atk_damage, attacker, defenderHero);
						defenderHero.DisplayDamageText(atk_damage);
						Debug.Log("Defend!");					

					}else {

					//Normal Damage	route
					atk_damage = attackersAttack - defendersDefense;
					
					DealDamage(atk_damage, attacker, defender);
					defender.DisplayDamageText(atk_damage);
					}				
					
		}

				//Revenge
		if (defender.hasRevenge)
		{
			
			if(!defender.GetComponentInParent<Player>().isActive){
				Attack(defender, attacker);
				Debug.Log("Revenge!");
			}
			
			
			// atk_damage = defendersAttack-attackersDefense;
			// //attacker.TakeDamage (atk_damage, defender);
			// DealDamage(atk_damage, defender, attacker);
			// Debug.Log("Revenge!");
			// attacker.DisplayDamageText(atk_damage);
		}
		
	}//Attack Status Checks

	//Checks for Critical Strike Flag and modifies attack damage
	public void CriticalStrikeCheck(HeroManager source, HeroManager target) {
		
		//For sure Critical
		if(source.hasCritical)
		{
			CriticalStrike(source, target);
		} else if(IsChanceSuccess(source))
		{
			CriticalStrike(source, target);
		} else 
		{
				attackersAttack = source.attack;
		}		
					
	}//Critical Strike Check

	public void CriticalStrike(HeroManager source, HeroManager target){

		//50% chance for critical strike to be x2 or x3 damage
		if (1-Random.value < 0.5)
		{
			attackersAttack = 2*source.attack;
		} 			
		else 
		{
			attackersAttack = 3*source.attack;
		}
		
		BattleTextMessage("Critical Strike: " +attackersAttack);
		
		target.hitByCritical = true;
		target.criticalSource = source;

		e_CriticalStrike();
		
	}//CriticalStrike	

	public void CheckThreat(HeroManager attacker, HeroManager defender){

		
		//For Defender
		//Check 3 states: 1) No Defender 2) Target has Defender 3) If you're target is an Ally
		if (NoThreat(defender.GetComponentInParent<Player>())|| defender.hasThreat|| defender.GetComponentInParent<Player>().tag == attacker.GetComponentInParent<Player>().tag ){

			canTargetHero = true;
			checkDefender = true;
		
		 }  else {

			 canTargetHero = false;
			 checkDefender = false;
			 Debug.Log ("Invalid Target: Attack Defender Only");
		 }	
	

	}//CheckDefender

	public void CheckTaunt(HeroManager attacker, HeroManager defender){
		
			
			if(attacker.GetComponent<Taunt>() != null ){
				if(attacker.GetComponent<Taunt>().source.GetComponent<HeroManager>() == defender || defender.GetComponentInParent<Player>().tag == attacker.GetComponentInParent<Player>().tag ){
					canTargetHero = true;	
					Debug.Log("attacker.GetComponent<Taunt>() != null");
				}	else{
					canTargetHero = false;
				}

			} else {
				CheckThreat(attacker, defender);
			}			
	}

	public void EndTurn(){
		CheckHealth ();
		DeselectAllHeroes ();

		//NextTurn();
		//StartCoroutine(PlayerTurnEnd());

	}

	public void ExtraTurn(HeroManager source)
	{

		if(!extraTurn){
			BattleTextMessage("EXTRA TURN!");
		
			//Set Extra Turn flag to TRUE and pause the GameManager
			extraTurn = true;
			isTurnPaused = true;

			if (ATBCoroutine != null)
			StopCoroutine (ATBCoroutine);
	 		StartATBCoroutine ();

			// List<HeroManager> extraTurnHeroes = AllyHeroList(source);

			// //Check which heroes are active during the extra turn
			// foreach(HeroManager extraTurnHero in extraTurnHeroes){

			// 	if(!extraTurnHero.hasExtraTurn)
			// 	{				
					

			// 		//Disable access to Skills	
			// 		extraTurnHero.heroPanel.SetActive(true);
			// 		List<Button> skillsButton = extraTurnHero.gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;					
			// 		for(int i = 0; i <skillsButton.Count; i++)
			// 		{
			// 			skillsButton[i].interactable = false;
						
			// 			skillsButton[i].GetComponent<Ability>().skillType = Type.ExtraTurn;
			// 		}				
			// 		extraTurnHero.gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);

			// 		//Grey Out heroes not available in extra turn
								
			// 		extraTurnHero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color = Color.grey;			
			

			// 	}//if

			// }//foreach	

		}	
	
	


	}//Extra Turn

	public void ExtraTurnCheck(HeroManager source){

		List<HeroManager> extraTurnHeroes = AllyHeroList(source);

		if(!isTurnPaused){	

			//NORMAL ROUTE - NO EXTRA TURN		

			//Resolve frozen heroes from an extra turn
			if(extraTurn){				

				foreach(HeroManager extraTurnhero in extraTurnHeroes){					

						//reset hasExtraturn back to normal
						extraTurnhero.hasExtraTurn = false;

						//Disable access to skills/abilities
						extraTurnhero.gameObject.GetComponent<HeroManager>().heroPanel.SetActive(true);
						List<Button> skillsButton = extraTurnhero.gameObject.GetComponentInChildren<HeroPanel>().skillsBtn;						
						for(int i = 0; i <skillsButton.Count; i++)
						{
							skillsButton[i].interactable = true;
							skillsButton[i].GetComponent<Ability>().skillType = extraTurnhero.abilityAssets[i].skillType;
							//hero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color = GameManager.Instance.origColor;
						}								
						extraTurnhero.gameObject.GetComponent<HeroManager>().heroPanel.SetActive(false);


						//Restore Original Color (grayed-out from Extra Turn)
						
						//hero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color = GameManager.Instance.origColor;
						extraTurnhero.transform.Find("HeroUI").gameObject.transform.Find("Image").GetComponent<Image>().color = Color.white;
										

					
				}//foreach
			}//if		
		
			//set Extra Turn to false and End the Turn
			extraTurn = false;

			source.GetComponentInParent<Player>().isEndTurn = true;

			EndTurn();

		} else {		
			
			//EXTRA TURN

			//Unpause GameManager during Extra Turn
			isTurnPaused = false;
			
		}
	}//Extra Turn Check

	public void ReviveHero(HeroManager target){

		target.gameObject.SetActive(true);
		DestroyAllBuffs(target);
		DestroyAllDebuffs(target);

		//run all passive and automatic abilities
		target.transform.Find("HeroPanel(Clone)").gameObject.SetActive(true);
								
				Ability[] abilities = target.GetComponentsInChildren<Ability>();
				foreach(Ability ability in abilities){
					if(ability.skillType == Type.Passive){
						ability.UseAbilityPassive();
					}
					//For active skills with Passive
					if(ability.skillType == Type.Active){
						ability.UseAbilityActive();
					}

					//reset all cooldowns
					ability.remainingCooldown = ability.abilityCooldown;
				}
		target.transform.Find("HeroPanel(Clone)").gameObject.SetActive(false);

		//setHealth back to original health
		target.maxHealth = target.origHealth;

		//set Glow Selector
		var image = target.glow.GetComponent<Image>().color;
		image.a = 1f;
		target.glow.GetComponent<Image>().color = image;

		//udpate UI
		target.UpdateUI();

	}//ReviveHero


	public void DestroyAllBuffs(HeroManager target){
		
		Buff[] buffs = target.GetComponents<Buff>();
		foreach(Buff buff in buffs)
		{
			Destroy(buff);
			target.UpdateUI();
		}//foreachBuff
	}//DestroyAllBuffs

	public void DestroyAllDebuffs(HeroManager target){
		
		Debuff[] debuffs = target.GetComponents<Debuff>();
		foreach(Debuff debuff in debuffs)
		{
			Destroy(debuff);
			target.UpdateUI();
		}//foreachBuff
	}//DestroyAllBuffs
	


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