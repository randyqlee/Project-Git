using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack all enemies with a Chance to Stun and chance to gain an extra turn.

public class RainOfStones : Ability {
	
	bool chanceSuccess = false;

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Chance extra turn
		bool extraTurn = GameManager.Instance.IsChanceSuccess(attacker);
		if(extraTurn){
			GameManager.Instance.ExtraTurn();
		} 
		
		GameManager.Instance.AttackAll(attacker, defender);

		//Stun all
		int targetCount = GameManager.Instance.EnemyHeroList(attacker).Count;
		UseAbilityRandom(attacker, defender, targetCount);

		

					

	}

}
