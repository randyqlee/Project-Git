using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  DreamyLeader : Ability {

	HeroManager hero, enemy;
	BuffPanel buffPanel;	
	bool stunEnemy;

	
	public override void UseAbility(HeroManager attacker, HeroManager defender){

		DreamyLeaderAbility();
		

	}
	
	
	// public override void UseAbilityPassive(){				

	// 	GameManager.Instance.e_NewTurn +=DreamyLeaderAbility;
	// 	Debug.Log("Dreamy Leader Subscribed");
		
	// 	base.UseAbilityPassive();

	// }//UsePassive

	// public override void DisableAbilityPassive(){

	// 	//Unsubscribe DreamyLeader here
	// 	GameManager.Instance.e_NewTurn -=DreamyLeaderAbility;
	// 	//GameManager.Instance.e_NextTurn -=DreamyLeaderAbility;		
	// 	base.DisableAbilityPassive();

	// }//Disable	


	//public void DreamyLeaderAbility(HeroManager attacker, HeroManager defender){	
	public void DreamyLeaderAbility(){			

		hero = gameObject.GetComponentInParent<HeroManager>();
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(hero);
		

		if(hero.GetComponentInParent<Player>().isActive){
			Debug.Log("Dreamy Leader Called");	

			foreach(HeroManager enemy1 in enemies){

				enemy = enemy1;
				if(enemy.GetComponents<Buff>() != null){

					Buff[] buffs = enemy.GetComponents<Buff>();

						if(buffs.Length >= 1)
					{
						stunEnemy = true;
						Buff buff = buffs[Random.Range(0,buffs.Length)];
						
						if(buff != null){
							Debug.Log("OnDestroy");
							buff.OnDestroy();
						}						
					}
					if(stunEnemy){
						if(enemy.hasImmunity || enemy.hasPermanentImmunity){

						}
						else {
							GameManager.Instance.AddDebuff("Stun", 1, hero, enemy);
							stunEnemy = false;
						}

					}
				}//if not null					
				
			}//foreach	

			

		}

	}//DreamyLeaderAbility

}
