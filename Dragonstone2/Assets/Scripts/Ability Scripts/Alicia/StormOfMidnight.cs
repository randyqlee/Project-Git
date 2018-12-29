using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormOfMidnight : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.AttackAll (attacker, defender);		
			
		List<HeroManager> enemyHeroList = GameManager.Instance.EnemyHeroList(attacker);
		
		//find highest defense among enemies
		int maxAttack = 0;
		int temp = 0;
		foreach (HeroManager enemyHero in enemyHeroList)
		{
			temp = enemyHero.attack;
			if (temp > maxAttack)
				maxAttack = temp;
		}

		int bonus = maxAttack;

		//deal damage to all enemies with the bonus
		foreach (HeroManager enemyHero in enemyHeroList)
		{
			GameManager.Instance.DealDamage (bonus, attacker, enemyHero);
		}


		// //TEST CODE
		// GameManager.Instance.Attack (attacker, defender);

		base.UseAbility(attacker, defender);



	}//UseAbility


}
