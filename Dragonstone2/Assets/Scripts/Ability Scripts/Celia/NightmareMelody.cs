using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nightmare Melody: Poison all enemies for 2 turns with a chance to Stun for 1 turn.



public class NightmareMelody : Ability {
	
public override void UseAbility(HeroManager attacker, HeroManager defender)
	{
		
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);
		int targetCount = enemies.Count;	

		//inflict poison for 2 turns
		foreach(HeroManager enemy in enemies){
			
			if(enemy.hasImmunity || enemy.hasPermanentImmunity){
				
				Debug.Log("Enemy Has Immunity");

			}else{

				//Debug.Log("Target Poisoned: " +enemy);
				GameManager.Instance.AddDebuff("Poison", 2, attacker, enemy);
			}
		}			

		//chance to Stun all		
		base.UseAbilityRandom(attacker, defender, targetCount);				

		//base.UseAbility(attacker);

	}

}
