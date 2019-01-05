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
		
		

		//make other heroes unavailable
		SelectHeroesForExtraTurn (attacker, defender);

		GameManager.Instance.DeselectAllHeroes();
		defender.SelectHero();
		
		GameManager.Instance.ExtraTurn();
		

		base.UseAbility();		

		

	}//UseAbility

	public void SelectHeroesForExtraTurn (HeroManager attacker, HeroManager defender)
	{				
		
			HeroManager[] heroes = attacker.GetComponentInParent<Player>().GetComponentsInChildren<HeroManager>();
			foreach(HeroManager hero in heroes)
			{			
				//condition
				if(hero == defender || hero == attacker){				
						//Do Nothing	 
				} else {
					GameManager.Instance.AddDebuff("ChargeVitalityStun", 1, attacker, hero);		 
				}			
			}//foreach		
	}//Selected Heroes Extra Turn

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
