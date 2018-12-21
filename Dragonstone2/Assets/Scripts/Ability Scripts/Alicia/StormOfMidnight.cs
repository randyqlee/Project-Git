using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormOfMidnight : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		// GameManager.Instance.AttackAll (attacker, defender);		
			
		// List<HeroManager> enemyHeroList = GameManager.Instance.EnemyHeroList(attacker);
		
		// //find highest defense among enemies
		// int maxDefense = 0;
		// int temp = 0;
		// foreach (HeroManager enemyHero in enemyHeroList)
		// {
		// 	temp = enemyHero.defense;
		// 	if (temp > maxDefense)
		// 		maxDefense = temp;
		// }

		// int bonus = maxDefense;

		// //deal damage to all enemies with the bonus
		// foreach (HeroManager enemyHero in enemyHeroList)
		// {
		// 	GameManager.Instance.DealDamage (bonus, attacker, defender);
		// }


		//TEST CODE
		GameManager.Instance.Attack (attacker, defender);

		base.UseAbility(attacker, defender);



	}//UseAbility


}
