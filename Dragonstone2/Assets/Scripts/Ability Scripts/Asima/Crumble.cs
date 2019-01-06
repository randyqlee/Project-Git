using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack and deal critical strike to all enemies.  
//Decrease Defense and Inflict Poison for 2 turns.

public class Crumble : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Attack and Deal Critical Strike to all enemies
		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.AttackAll(attacker, defender);
		Debug.Log("Attacker Critical Damage: " +GameManager.Instance.atk_damage);
		attacker.hasCritical = criticalStatus;

		//Deal decrease defense and Poison to all enemies
		float chanceStatus = attacker.chance;
		attacker.chance = 1000f;
		int enemyCount = GameManager.Instance.EnemyHeroList(attacker).Count;
		UseAbilityRandom(attacker, defender, enemyCount);
		attacker.chance = chanceStatus;


		base.UseAbility(attacker);

		

	}

	

}
