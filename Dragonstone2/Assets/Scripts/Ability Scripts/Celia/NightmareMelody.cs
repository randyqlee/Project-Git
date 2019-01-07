using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareMelody : Ability {

	// /Nightmare Melody: Stun all enemies for 1 turn with a chance to inflict Poison for 2 turns.

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//Stun enemies
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);
		foreach(HeroManager enemy in enemies)
		{
			if(enemy.hasImmunity || enemy.hasPermanentImmunity){
				Debug.Log("Enemy is Immune");
			} else {
				GameManager.Instance.AddDebuff("Stun", 1, attacker, enemy);
			}
		}//foreach

		//chance for Poison
		base.UseAbilityRandom(attacker, defender, enemies.Count);

	}//UseAbility
}//class
