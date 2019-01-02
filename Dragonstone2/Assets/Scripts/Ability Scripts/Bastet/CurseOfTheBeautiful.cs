using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Critical Strike attack all enemies and Inflict Decrease Defense, Crippled Strike, 
//and chance to Unhealable for 2 turns.

public class CurseOfTheBeautiful : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//Critical strike all enemies
		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.AttackAll(attacker, defender);
		attacker.hasCritical = criticalStatus;

		//Apply Decrease Defense and Crippled Strike
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);

		foreach(HeroManager enemy in enemies){
			if(!(enemy.hasImmunity || enemy.hasPermanentImmunity)) {

			GameManager.Instance.AddDebuff("DecreaseDefense", 2, attacker, enemy);			
			GameManager.Instance.AddDebuff("CrippledStrike", 2, attacker, enemy);			
			}
			

		}//foreach
	
		
		//ChanceUnhealable
		UseAbilityRandom(attacker, defender, enemies.Count);

		

	}

}
