using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gouge : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{	
		
		//attack and deal critical strike to all enemies
		GameManager.Instance.AttackAllCritical(attacker, defender);

		//chance to Poison
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);		
		UseAbilityRandom(attacker, defender, enemies.Count);

		//Deal Bonus Damage
		foreach(HeroManager enemy in enemies){

			if(enemy.GetComponents<Debuff>() != null){			
			Debuff[] debuffs = enemy.GetComponents<Debuff>();
			foreach(Debuff debuff in debuffs){				
				if(debuff.debuff.name == "Poison"){
					//there can only be one Stun Debuff at a time					
					int bonusDamage = attacker.defense;
					GameManager.Instance.DealDamage(bonusDamage, attacker, enemy);
					Debug.Log("Gouge Bonus Damage: " +bonusDamage +" " +enemy);
					}//if
				}//foreach			
			}//if			

		}	

	}

}
