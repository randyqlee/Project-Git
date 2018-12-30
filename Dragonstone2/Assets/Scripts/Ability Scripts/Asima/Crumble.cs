using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crumble : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Attack and Deal Critical Strike to all enemies
		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.AttackAll(attacker, defender);
		attacker.hasCritical = criticalStatus;

		//Deal decrease defense and Poison to all enemies
		float chanceStatus = attacker.chance;
		attacker.chance = 1000f;
		int enemyCount = GameManager.Instance.EnemyHeroList(attacker).Count;
		UseAbilityRandom(attacker, defender, enemyCount);
		attacker.chance = chanceStatus;


		base.UseAbility();

		

	}

	

}
