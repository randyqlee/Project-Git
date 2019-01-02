using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisBlessing : Ability {

//Each ally gains a Shield, Lucky, and Increase Attack for 3 turns.
	

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		int targetCount;

		targetCount = GameManager.Instance.AllyHeroList(attacker).Count;

		UseAbilityRandom(attacker, defender, targetCount);	

		

	}

}
