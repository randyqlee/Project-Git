using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ventilate : Ability {

	void Start(){
		cantReduce = true;
	}
	
	public override void UseAbility(HeroManager attacker, HeroManager defender)
	{
		
		//Reset Skills to Max Cooldown
		defender.heroPanel.SetActive(true);
		Ability[] abilities = defender.GetComponentsInChildren<Ability>();
		foreach (Ability ability in abilities){
			
			ability.RefreshCooldown();
		}
		defender.heroPanel.SetActive(false);

		base.UseAbility();
		
	}


}
