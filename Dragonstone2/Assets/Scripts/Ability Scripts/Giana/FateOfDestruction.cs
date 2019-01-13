using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fate of Destruction: Inflict Poison and Bomb for 2 turns to an enemy.  
//Chance to gain an extra turn.

public class FateOfDestruction : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{	
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
		
		foreach(HeroManager ally in allies){
			ally.hasExtraTurn = true;			
		}
		
		GameManager.Instance.ExtraTurn(attacker);
		base.UseAbility(attacker, defender);		

	}

}
