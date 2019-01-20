using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedPromise : Ability 
{
//Delayed Promise: Increase skill cooldowns of enemies by 1 with a 
//chance to stun each enemy for 1-2 turns.	

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{		
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);

		foreach(HeroManager enemy in enemies){

			//Stun for 1-2 turns
			if(GameManager.Instance.IsChanceSuccess(attacker)){
				if(enemy.hasImmunity || enemy.hasPermanentImmunity){
					Debug.Log("Enemy has Immunity");
				}else{
					GameManager.Instance.AddDebuff("Stun", Random.Range(1,3), attacker, enemy);
				}
			}


			//Increase ability cooldowns by 1
			enemy.heroPanel.SetActive(true);
			Ability[] abilities = enemy.GetComponentsInChildren<Ability>();			
			foreach(Ability ability in abilities){
				Debug.Log("Ability: " +ability);
				ability.remainingCooldown++;
			}//foreach ability
			enemy.heroPanel.SetActive(false);

		}//foreach enemy

		base.UseAbility(attacker, defender);

	}//UseAbility

}
