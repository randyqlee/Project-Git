using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack all enemies with a Chance to Stun and chance to gain an extra turn.

public class RainOfStones : Ability {
	
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

		base.UseAbility();			

	}

}
