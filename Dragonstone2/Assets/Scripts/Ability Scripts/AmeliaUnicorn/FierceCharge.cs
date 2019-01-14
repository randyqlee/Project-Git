using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fierce Charge: Gain Revenge for 2 turns and attack an enemy with a chance to Taunt for 1 turn.  Deal additonal 80 damage.


public class FierceCharge : Ability {

	int bonusDamage = 80;

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{	
		
		//Gain Revenge for 2 turns
		GameManager.Instance.AddBuff("Revenge", 2, attacker, attacker);

		//attack
		GameManager.Instance.Attack(attacker, defender);

		//deal 80 damage
		GameManager.Instance.DealDamage(bonusDamage, attacker, defender);
		
		//chance to Taunt
		base.UseAbility(attacker, defender);		

	}

}
