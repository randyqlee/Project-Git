﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Critical Attack an enemy, remove a buff, and set all of its skills to MAX cooldown.


public class SpearOfDevastation : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Remove a buff
		Buff[] buffs = defender.GetComponents<Buff>();

		if(buffs.Length > 0){
			Debug.Log("Buffs: " +buffs[0]);
			Buff buff = buffs[Random.Range(0,buffs.Length)];
			//buff.OnDestroy();
			Destroy(buff);

		}

		//Reset defender abilities to MAX cooldown
		defender.heroPanel.SetActive(true);
		Ability[] abilities = defender.GetComponentsInChildren<Ability>();
		foreach (Ability ability in abilities){
			//Debug.Log("Abiltiies: " +ability.name);
			ability.MaxCooldown();
		}

		defender.heroPanel.SetActive(false);

		//Critical Attack an Enemy
		GameManager.Instance.AttackCritical(attacker, defender);

		base.UseAbility(attacker);		

	}

}
