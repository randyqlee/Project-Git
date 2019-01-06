using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class  FeastOfBlood : Ability {

	public override void UseAbilityPassive(){	

		base.UseAbilityPassive();

	}//UsePassive

	public override void DisableAbilityPassive(){		

		base.DisableAbilityPassive();

	}//Disable

	public override void UseAbility(HeroManager attacker, HeroManager defender){

		//Debug.Log("Feast of Blood Ability");
			//Add Brand
			
			if((defender.hasImmunity || defender.hasPermanentImmunity)){
				Debug.Log ("Target Hero has immunity");

			}else{
				GameManager.Instance.AddDebuff("Brand", 2, attacker, defender);
				Debug.Log("Feast of Blood - Brand");	
			}			
			//Heal all for 30
			int healValue = 30;
			List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
			foreach(HeroManager ally in allies){
				GameManager.Instance.Heal(ally, healValue);
				
			}
			//Extra Turn		
		GameManager.Instance.CheckHealth();
		//Extra Turn
		bool deadStatus = defender.gameObject.activeInHierarchy;
		defender.gameObject.SetActive(true);
		
		if(defender.isDead){
			foreach(HeroManager ally in allies){
				ally.hasExtraTurn = true;
			}
			GameManager.Instance.ExtraTurn(attacker);
		} 
		defender.gameObject.SetActive(deadStatus);
		
		}	

	}

	

	


