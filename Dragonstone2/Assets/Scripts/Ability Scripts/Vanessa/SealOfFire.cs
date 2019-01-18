using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Seal of Fire: Attack an enemy, deal 100 bonus damage, and 
//decrease its Defense for 2 turns. 

public class SealOfFire : Ability {

	int bonusDamage = 100;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack(attacker, defender);	

		GameManager.Instance.DealDamage(bonusDamage, attacker, defender);
		
		base.UseAbility(attacker, defender);

	}
}
