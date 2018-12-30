using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gouge : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		
		
		//attack and deal critical strike to all enemies
		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.AttackAll(attacker, defender);
		attacker.hasCritical = criticalStatus;

		//chance to Poison
		int enemyCount = GameManager.Instance.EnemyHeroList(attacker).Count;
		GameManager.Instance.isTurnPaused = true;
		UseAbilityRandom(attacker, defender, enemyCount);


		//Deal Bonus Damage
		if(defender.GetComponents<Debuff>() != null){			
			Debuff[] debuffs = defender.GetComponents<Debuff>();
			foreach(Debuff debuff in debuffs){				
				if(debuff.debuff.name == "Poison"){
					//there can only be one Stun Debuff at a time					
					int bonusDamage = attacker.defense;
					GameManager.Instance.DealDamage(bonusDamage, attacker, defender);
					Debug.Log("Gouge Bonus Damage: " +bonusDamage);
				}//if
			}//foreach			
		}//if		

		base.UseAbility();

		

	}

}
