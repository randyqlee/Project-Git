using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Critical Error: Attack all enemies and remove all buffs.  
//Chance to Stun enemies whose beneficial effect has been removed for 1 turn. 

public class CriticalError : Ability {

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		List<HeroManager> enemies = GameManager.Instance.EnemyHeroList(attacker);

		//attack all enemies
		GameManager.Instance.AttackAll(attacker, defender);

		if(attacker.GetComponentInParent<Player>().isActive){			
		
			foreach(HeroManager enemy in enemies){
				//Remove a buff
				Buff[] buffs = enemy.GetComponents<Buff>();

				if(buffs.Length > 0){
					foreach(Buff buff in buffs){
						//buff.OnDestroy();					
						Destroy(buff);
						//Debug.Log("Buff Destroyeda " +buff +" from hero: " +enemy );
					}				




					//addstun
					if(enemy.hasImmunity || enemy.hasPermanentImmunity){
						
						Debug.Log("Has Immunity");

					}else{

						if(GameManager.Instance.IsChanceSuccess(attacker))
						GameManager.Instance.AddDebuff("Stun", 1, attacker, enemy);
					}				

				}
			}		

		}//if playerisActive	


		base.UseAbility(attacker);
		

	}

}
