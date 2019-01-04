using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMagic : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Attack an enemy
		GameManager.Instance.Attack(attacker, defender);

		//Reset Skills to Max Cooldown
		defender.heroPanel.SetActive(true);
		Ability[] abilities = defender.GetComponentsInChildren<Ability>();
		foreach (Ability ability in abilities){
			//Debug.Log("Abiltiies: " +ability.name);
			ability.ResetCooldown();
		}
		defender.heroPanel.SetActive(false);

		//Gain an Extra turn
		GameManager.Instance.ExtraTurn();

		base.UseAbility();



	}//UseAbility


}
