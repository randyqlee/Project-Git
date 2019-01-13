using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsDance : Ability {

//Dragon's Dance:  Critical attack all enemies with a chance to stun for 1 turn.  
//Remove 1 buff per enemy.



public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		//Critical Attack all enemies
		GameManager.Instance.AttackAll(attacker, defender);		

		//Remove 1 random buff per enemy
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);
		foreach(HeroManager enemy in enemies){
			
			Buff[] buffs = enemy.GetComponents<Buff>();

			if(buffs.Length > 0){
				Buff buff = buffs[Random.Range(0, buffs.Length)];
				//Debug.Log("Buff Removed from " +enemy.name +": " +buff.buff.name);
				Destroy(buff);
			}
		}


		
		
		
		//chance to stun for 1 turn
		int targetCount = GameManager.Instance.EnemyHeroList(attacker).Count;
		base.UseAbilityRandom(attacker, defender, targetCount);		

	}	

}//Ability
