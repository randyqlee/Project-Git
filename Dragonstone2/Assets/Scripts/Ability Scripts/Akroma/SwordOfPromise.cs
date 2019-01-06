using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfPromise : Ability {

	int targetCount;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//Debug.Log ("Using SwordOfPromise: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

		GameManager.Instance.Attack (attacker, defender);

		GameManager.Instance.CheckHealth();
		if(defender.isDead){
			SealOfLightTrigger(attacker, defender);			
		} else {
			ResetCooldown();
			GameManager.Instance.ExtraTurnCheck(attacker);			
		}
		
		

	}//Override UseAbility
	//Seal of Light Replica
	void SealOfLightTrigger(HeroManager attacker, HeroManager defender) {

		targetCount = GameManager.Instance.EnemyHeroList(attacker).Count;		
		GameManager.Instance.AttackAll (attacker, defender);			
		base.UseAbilityRandom(attacker, defender, targetCount);
		Debug.Log("Seal of Light trigger");
	}
}
