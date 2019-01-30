using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Decrease defense of all enemies for 3 turns, then Attack all enemies.  


public class WaterGuardianAngel : Ability {
	
	

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		int enemyCount = GameManager.Instance.EnemyHeroList(attacker).Count;

		base.UseAbilityRandom(attacker, defender, enemyCount);						

		GameManager.Instance.AttackAll(attacker, defender);

	}

}
