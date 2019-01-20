using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Heavenly Sword: Attack an enemy and deal additional damage equal to your defense, with a chance to remove a buff.

public class HeavenlySword : Ability {

	int bonusDamage;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//attack
		GameManager.Instance.Attack (attacker, defender);

		//bouns Damage
		bonusDamage = attacker.defense;
		GameManager.Instance.DealDamage(bonusDamage, attacker, defender);

		//Remove Random Buff
		if(GameManager.Instance.IsChanceSuccess(attacker)){
			Buff[] buffs = defender.GetComponents<Buff>();
			Buff randomBuff = buffs[Random.Range(0, buffs.Length)];
			Destroy(randomBuff);
		}
	}//Override UseAbility	
}
