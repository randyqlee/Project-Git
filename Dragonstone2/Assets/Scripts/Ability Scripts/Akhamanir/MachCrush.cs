using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachCrush : Ability {

	int bonus = 50;

	public override void UseAbility ()
	{
		Debug.Log ("Using  MachCrush");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using  MachCrush");

			GameManager.Instance.AttackAll (attacker, defender);
			
		List<HeroManager> enemyHeroList = GameManager.Instance.EnemyHeroList(attacker);
		foreach (HeroManager enemyHero in enemyHeroList)
		{
			if (enemyHero.GetComponents<Debuff>() != null)
			{
				GameManager.Instance.DealDamage (bonus * enemyHero.GetComponents<Debuff>().Length, attacker, defender);
			}
		}

	}
}

