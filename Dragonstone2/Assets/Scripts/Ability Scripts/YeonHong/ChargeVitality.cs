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
		
		//Reduce Cooldowns by 1
		//YeonHong CD reduction
		Ability[] attackerAbilities = attacker.GetComponentsInChildren<Ability>();

		foreach(Ability attackerAbility in attackerAbilities){
			attackerAbility.remainingCooldown--;
			attackerAbility.GetComponentInChildren<Text>().text = attackerAbility.remainingCooldown.ToString();		
			if(attackerAbility.remainingCooldown < 0){
				attackerAbility.remainingCooldown = 0;
				attackerAbility.GetComponentInChildren<Text>().text = attackerAbility.remainingCooldown.ToString();		
			}					
		}//foreach

		//Ally target CD reduction
		defender.GetComponent<HeroManager>().heroPanel.SetActive(true);
		Ability[] defenderAbilities = defender.GetComponentsInChildren<Ability>();
		
		foreach(Ability defenderAbility in defenderAbilities){
			defenderAbility.remainingCooldown--;
			defenderAbility.GetComponentInChildren<Text>().text = defenderAbility.remainingCooldown.ToString();		
			Debug.Log("defenderAbilities: " +defenderAbility.name);
			if(defenderAbility.remainingCooldown < 0){
				defenderAbility.remainingCooldown = 0;
				defenderAbility.GetComponentInChildren<Text>().text = defenderAbility.remainingCooldown.ToString();		
			}					
		}//foreach
		defender.GetComponent<HeroManager>().heroPanel.SetActive(false);

		GameManager.Instance.DeselectAllHeroes();
		defender.SelectHero();
		
		GameManager.Instance.ExtraTurn();
		base.UseAbility(attacker, defender);

	}//UseAbility

	

}//Charge Vitality
