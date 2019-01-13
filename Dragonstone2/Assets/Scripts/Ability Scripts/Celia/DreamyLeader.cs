using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dreamy Leader (Passive): Removes a random buff on an enemy every turn and 
//stun it for 1 turn if a buff is removed this way. [Automatic Effect]


public class DreamyLeader : Ability {

	HeroManager hero;

	public override void UseAbilityPassive(){

		GameManager.Instance.e_PlayerStartPhase += DreamyLeaderAbility;

	}

	public override void DisableAbilityPassive(){

		GameManager.Instance.e_PlayerStartPhase -= DreamyLeaderAbility;

	}

	public override void UseAbility(HeroManager attacker, HeroManager defender){

		DreamyLeaderAbility();
	}
	
	
	public void DreamyLeaderAbility ()
	{
		hero = gameObject.GetComponentInParent<HeroManager>();
	
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(hero);
		int targetCount = enemies.Count;
		
		if(hero.GetComponentInParent<Player>().isActive){			
		
			foreach(HeroManager enemy in enemies){
				//Remove a buff
				Buff[] buffs = enemy.GetComponents<Buff>();

				if(buffs.Length > 0){
					Buff buff = buffs[Random.Range(0,buffs.Length)];
					
					//buff.OnDestroy();					
					Destroy(buff);
					//Debug.Log("Buff Destroyeda " +buff +" from hero: " +enemy );

					//addstun
					if(enemy.hasImmunity || enemy.hasPermanentImmunity){
						
						Debug.Log("Has Immunity");

					}else{
						GameManager.Instance.AddDebuff("Stun", 1, hero, enemy);
					}
					

				}

			}		

		}//if playerisActive		

		//base.UseAbility(attacker);		

	}

}//class
