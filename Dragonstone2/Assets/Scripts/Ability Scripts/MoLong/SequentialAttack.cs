using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialAttack : Ability {

//Critical attack an enemy with a chance to Decrease Defense for 2 turns.

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		GameManager.Instance.AttackCritical(attacker, defender);	
		
		base.UseAbility(attacker, defender);		

	}	

}//Ability
