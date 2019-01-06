using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack all enemies with a Chance to Stun and chance to gain an extra turn.

public class RainOfStones : Ability {
	
	bool chanceSuccess = false;

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Get allies list
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);

		//Chance extra turn
		bool extraTurn = GameManager.Instance.IsChanceSuccess(attacker);
		if(extraTurn){
			foreach(HeroManager ally in allies){
				ally.hasExtraTurn = true;
			}
			GameManager.Instance.ExtraTurn(attacker);
		} 
		
		GameManager.Instance.AttackAll(attacker, defender);

		//Stun all
		int targetCount = GameManager.Instance.EnemyHeroList(attacker).Count;
		UseAbilityRandom(attacker, defender, targetCount);

		

					

	}

}
