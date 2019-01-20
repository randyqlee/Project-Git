using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attack an enemy with a chance to Decrease Attack for 2 turns.

public class TouchOfSeductionWind : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{		
		GameManager.Instance.Attack(attacker, defender);

		base.UseAbility(attacker, defender);		

	}

}
