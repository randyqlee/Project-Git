using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemptingDream : Ability {

	//Tempting Dream: Attack an enemy with a chance to stun for 1 turn. Deal additonal damage equivalent to your chance.
	
	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//attack
		GameManager.Instance.Attack (attacker, defender);

		//bonus damage
		int bonusDamage = (int)attacker.chance;
		Debug.Log("Bonus Damage: " +bonusDamage);
		defender.TakeDamage(bonusDamage, attacker);

		//chance stun
		base.UseAbility(attacker, defender);		
		

	}//Override UseAbility
	
}
