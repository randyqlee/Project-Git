using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Judge: Counterattack when an enemy critical strikes an ally.  
//Gain Increase Defense and Lucky for 2 turns whenever this skill is activated.

public class  Judge : Ability {	

	HeroManager hero;
	
	public override void UseAbilityPassive(){		

		GameManager.Instance.e_CriticalStrike += JudgeAbility;		
		base.UseAbilityPassive();

	}//UsePassive

	public override void DisableAbilityPassive(){

		GameManager.Instance.e_CriticalStrike -= JudgeAbility;
		base.DisableAbilityPassive();

	}//Disable

	public void JudgeAbility(){

		hero = GetComponentInParent<HeroManager>();
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(hero);

		foreach(HeroManager ally in allies){

			if(ally.hitByCritical){				
				HeroManager enemyTarget = ally.criticalSource;		

				int attackersAttack;
				if(hero.hasCritical){
					if (1-Random.value < 0.5)
					{
						attackersAttack = 2*hero.attack;
					} 			
					else 
					{
						attackersAttack = 3*hero.attack;
					}
					GameManager.Instance.BattleTextMessage("Critical Strike: " +attackersAttack);
					
					enemyTarget.hitByCritical = true;
					enemyTarget.criticalSource = hero;		

				}else{
					attackersAttack = hero.attack;
				}

				//prevent infinite bounce
				GameManager.Instance.DealDamage(attackersAttack, hero, enemyTarget);
				
				GameManager.Instance.BattleTextMessage("Judge: " +enemyTarget);	
				Debug.Log("Judge: " +enemyTarget);
							
			}//if ally hitByCritical
		}//foreach

	}

}
