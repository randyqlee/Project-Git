using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tempting Dream: Attack an enemy with a chance to stun for 1 turn. 
//Deal additonal damage equivalent to your chance.

public class TemptingDreamWind : Ability {

//chance to stun
public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack (attacker, defender);		

		int damage = (int)attacker.chance;

		GameManager.Instance.DealDamage(damage, attacker, defender);	

		base.UseAbility(attacker, defender);

	}	
}//Ability
