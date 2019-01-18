using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  WarriorsReturn : Ability {
	
	HeroManager hero;	
	
	public override void UseAbilityPassive(){	

		GameManager.Instance.e_PlayerStartPhase += WarriorsReturnAbility;
	}//UsePassive

	public override void DisableAbilityPassive(){	

		GameManager.Instance.e_PlayerStartPhase -= WarriorsReturnAbility;
	}//Disable

	public void WarriorsReturnAbility(){

			
		hero = GetComponentInParent<HeroManager>();
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(hero);

		List<HeroManager> deadAllies = new List<HeroManager>();

		//Get list of dead allies
		if(remainingCooldown == 0){
			foreach(HeroManager ally in allies){
				if(ally.isDead){
					Debug.Log("Warriors Ability");
					deadAllies.Add(ally);					
				}//if ally is dead
			}//foreach						
		}//if Remaining Cooldown

		// HeroManager deadAllyRevive = deadAllies[Random.Range(0,deadAllies.Count)];

		// deadAllyRevive.maxHealth = 400;
		// deadAllyRevive.isDead = false;
		// deadAllyRevive.gameObject.SetActive(true);		

	}//Warriors Return Ability
	

}
