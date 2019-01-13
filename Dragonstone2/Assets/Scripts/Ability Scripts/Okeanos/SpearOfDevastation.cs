using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Critical Attack an enemy, remove a buff, and set all of its skills to MAX cooldown.


public class SpearOfDevastation : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		
		//Critical Attack an Enemy
		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.Attack(attacker, defender);
		attacker.hasCritical = criticalStatus;

		//Remove a buff
		Buff[] buffs = defender.GetComponents<Buff>();
		if(buffs.Length > 0){
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

		base.UseAbility(attacker);		

	}

}
