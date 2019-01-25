using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reckless Assault:  Deal 500 damage to an enemy and 400 damage to you.  If this ability will kill you, set your Hp to 1 and gain endure for 2 turns.

public class RecklessAssault : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		int recklessAssaultDamageEnemy = 500;
		int recklessAssaultDamageSelf = 400;
		
		GameManager.Instance.DealDamage(recklessAssaultDamageEnemy, attacker, defender);
		GameManager.Instance.DealDamage(recklessAssaultDamageSelf, attacker, attacker);

		if(attacker.maxHealth < 0){
			attacker.maxHealth = 1;

			string endure = "Endure";

			if(attacker.hasAntiBuff){
				Debug.Log("Can't be Buffed");
			}else{
				GameManager.Instance.AddBuff(endure, 2, attacker, attacker);
			}
			
		}			
		
		base.UseAbility(attacker);

		

	}

}
