using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Critical Attack an enemy, remove a buff, and set all of its skills to MAX cooldown.


public class RecklessAssault : Ability {



public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		int recklessAssaultDamageEnemy = 500;
		int recklessAssaultDamageSelf = 400;
		
		defender.TakeDamage(recklessAssaultDamageEnemy, attacker);
		attacker.TakeDamage(recklessAssaultDamageSelf, attacker);

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
