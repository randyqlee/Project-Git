using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Sword of Promise: Attacks with a sword. 
//[Seal of Fire] is automatically activated if the enemy dies.

public class SwordOfPromiseFire : Ability {

	int targetCount;
	List<HeroManager> enemies;
	

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack (attacker, defender);

		GameManager.Instance.CheckHealth();
		if(defender.isDead){
			SealOfFireTrigger(attacker, defender);			
		} 		

		base.UseAbility(attacker, defender);		

	}
	
	//Seal of Light Replica
	public void SealOfFireTrigger(HeroManager attacker, HeroManager defender) {

		enemies = GameManager.Instance.EnemyHeroList(attacker);		
		targetCount = enemies.Count;

		HeroManager randomDefender = enemies[Random.Range(0,targetCount)];

		SealOfFire sealOfFire = gameObject.GetComponentInParent<HeroManager>().GetComponentInChildren<SealOfFire>();

		sealOfFire.UseAbility(attacker, randomDefender);

	}


}
