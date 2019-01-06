using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachCrush : Ability {

	int bonus = 50;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//Debug.Log ("Using  MachCrush");

		GameManager.Instance.AttackAll (attacker, defender);
			
		List<HeroManager> enemyHeroList = GameManager.Instance.EnemyHeroList(attacker);
		foreach (HeroManager enemyHero in enemyHeroList)
		{
			
			int bonusDamage = bonus * enemyHero.GetComponents<Debuff>().Length;			
			//int totalDamage = bonusDamage + attacker.attack - enemyHero.defense;
			
			GameManager.Instance.DealDamage (bonusDamage, attacker, enemyHero);
			
		}

		base.UseAbility(attacker, defender);
	}//UseAbility
}

