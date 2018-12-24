using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeVitality : Ability {

	public override void UseAbility(HeroManager attacker, HeroManager defender){

		GameManager.Instance.Attack(attacker, defender);
		base.UseAbility(attacker, defender);		
	}

}
