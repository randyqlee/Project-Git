using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Human Horn:  Attack an enemy with a chance to stun.


public class HumanHorn : Ability {

	

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	
	{
		GameManager.Instance.Attack(attacker, defender);

		

		base.UseAbility(attacker, defender);

	}	

}
