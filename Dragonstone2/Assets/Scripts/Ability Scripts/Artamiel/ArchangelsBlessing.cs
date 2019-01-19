using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchangelsBlessing : Ability {

	int targetCount;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//Debug.Log ("Using SealOfLight: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);
		targetCount = GameManager.Instance.EnemyHeroList(attacker).Count;		

		GameManager.Instance.AttackAll (attacker, defender);	
		
		base.UseAbilityRandom(attacker, defender, targetCount);

	}
}
