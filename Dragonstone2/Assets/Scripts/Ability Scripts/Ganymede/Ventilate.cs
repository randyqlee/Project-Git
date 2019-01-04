using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ventilate : Ability {
	
	

	public override void UseAbility(HeroManager attacker, HeroManager defender)
	{
		
		//Reset Skills to Max Cooldown
		defender.heroPanel.SetActive(true);
		Ability[] abilities = defender.GetComponentsInChildren<Ability>();
		foreach (Ability ability in abilities){
			//Debug.Log("Abiltiies: " +ability.name);
			ability.remainingCooldown = 0;
			ability.gameObject.GetComponentInChildren<Text>().text = remainingCooldown.ToString();		
		}
		defender.heroPanel.SetActive(false);

		base.UseAbility();
		
	}


}
