using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serenity : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		GameManager.Instance.AttackCritical (attacker, defender);


		base.UseAbility(attacker, defender);

		

	}

}
