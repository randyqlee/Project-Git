using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMeTeddy : Ability {



public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack (attacker, defender);			

		//Apply stun
		ApplyDebuff(attacker, defender);

		//Check if enemy is stunned
		if(defender.GetComponents<Debuff>() != null){
			Debug.Log("Debuff Exists" );
			
			Debuff[] debuffs = defender.GetComponents<Debuff>();

			foreach(Debuff debuff in debuffs){
				Debug.Log("Debuff Name: " +debuff.debuff.name);
				if(debuff.debuff.name == "Stun"){
					//there can only be one Stun Debuff at a time					
					StunBonusDamage(attacker, defender);
				}//if
			}//foreach			
		}//if		
		
		base.UseAbility();		

	}

	void ApplyDebuff(HeroManager attacker, HeroManager defender){
		GameManager.Instance.CheckTaunt(attacker, defender);

		if(GameManager.Instance.canTargetHero){
			
			if (abilityDebuffs != null)
			{
				foreach (AbilityDebuffs abilityDebuffs in abilityDebuffs)
				{
					GameManager.Instance.AddDebuffComponent(abilityDebuffs.debuff.ToString(),abilityDebuffs.duration,attacker,defender);

				}
			}
		}
	}//Apply Debuff

	void StunBonusDamage(HeroManager attacker, HeroManager defender){
		//Deal additional attack damage when stunned
		int bonusDamage = attacker.attack;
		GameManager.Instance.DealDamage(bonusDamage, attacker, defender);
		Debug.Log("Help Me Teddy Bonus Damage");
	}

}//Ability
