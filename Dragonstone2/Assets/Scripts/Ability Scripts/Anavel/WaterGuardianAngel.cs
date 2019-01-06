using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decrease defense of all enemies for 3 turns, then Attack all enemies.  
//Chance for Critical Strike.

public class WaterGuardianAngel : Ability {
	
	bool chanceSuccess = false;

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		GameManager.Instance.isTurnPaused = true;

		int enemyCount = GameManager.Instance.EnemyHeroList(attacker).Count;

		base.UseAbilityRandom(attacker, defender, enemyCount);	

		chanceSuccess = GameManager.Instance.IsChanceSuccess(attacker);

		if(chanceSuccess){
			//store status of Critical
			bool criticalStatus = attacker.hasCritical;
			attacker.hasCritical = true;
			GameManager.Instance.AttackAll(attacker, defender);
			//restore original state of hasCritical
			attacker.hasCritical = criticalStatus;	
		} else {

			GameManager.Instance.AttackAll(attacker, defender);

		}

		base.UseAbility(attacker);			

	}

}
