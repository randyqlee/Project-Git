using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ice Horn: Attack an enemy and deal additional 80 damage with a chance to stun for 1 turn.  


public class IceHorn : Ability {

	int bonusDamage = 80;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	
	{
		GameManager.Instance.Attack(attacker, defender);

		GameManager.Instance.DealDamage(bonusDamage, attacker, defender);

		base.UseAbility(attacker, defender);

	}	

}
