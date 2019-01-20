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
				GameManager.Instance.DealDamage(hero.attack, hero, enemyTarget);
				//GameManager.Instance.Attack(hero, enemyTarget);
				GameManager.Instance.BattleTextMessage("Judge: " +enemyTarget);	
				Debug.Log("Judge: " +enemyTarget);
							
			}//if ally hitByCritical
		}//foreach

	}

}
