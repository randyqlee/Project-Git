using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeVitality : Ability {

	//Heal ally by 500, your and an ally target's cooldown by 1, and take an extra turn.	

	int healValue = 500;

	public override void UseAbility(HeroManager attacker, HeroManager defender){

		//Heal
		GameManager.Instance.Heal(defender, healValue);
		
		//Reduce Cooldown of YeonHeong and target Ally by 1
		//SelectHeroesForSkillCooldown(attacker, defender);
		YeonReduceCooldown(attacker, defender);
		
		
		//SelectHeroesForExtraTurn (attacker, defender);
		GameManager.Instance.DeselectAllHeroes();
		defender.SelectHero();


		//Get allies list and set who will have extra turn
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
		//use static nature of GameManager to store the list in the variable there, as opposed to passing arguements
		GameManager.Instance.extraTurnHeroes = allies;

		//Select which ally shall have an extra turn
		foreach(HeroManager ally in GameManager.Instance.extraTurnHeroes){
			if(ally == attacker || ally == defender){
				ally.hasExtraTurn = true;
			}//if
		}//foreach		
		GameManager.Instance.ExtraTurn();

		base.UseAbility();				

	}//UseAbility


	public void YeonReduceCooldown(HeroManager attacker, HeroManager defender){
		

		defender.heroPanel.SetActive(true);
		Ability[] abilities = defender.GetComponentsInChildren<Ability>();
		foreach (Ability ability in abilities){
			//Debug.Log("Abiltiies: " +ability.name);
			ability.ReduceCooldown(1);
		}
		defender.heroPanel.SetActive(false);

		attacker.heroPanel.SetActive(true);
		abilities = attacker.GetComponentsInChildren<Ability>();
		foreach (Ability ability in abilities){
			//Debug.Log("Abiltiies: " +ability.name);
			ability.ReduceCooldown(1);
		}
		attacker.heroPanel.SetActive(false);



	}



}//Charge Vitality
