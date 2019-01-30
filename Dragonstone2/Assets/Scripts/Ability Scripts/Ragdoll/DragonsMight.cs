using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dragon's Might: Attack an enemy with a chance to critical strike. 
//Inflict Poison for 2 turns if you land a Critical Hit.


public class DragonsMight : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	
	{
		if(GameManager.Instance.IsChanceSuccess(attacker)){

			//chance to critical
			GameManager.Instance.AttackCritical(attacker, defender);
			

			//gain lucky
			if(defender.hasImmunity || defender.hasPermanentImmunity){
				Debug.Log("Target Has Immunity");
			} else {
				GameManager.Instance.AddDebuff("Poison", 2, attacker, defender);
			}
			
		}

		base.UseAbility(attacker);		

	}	

}
