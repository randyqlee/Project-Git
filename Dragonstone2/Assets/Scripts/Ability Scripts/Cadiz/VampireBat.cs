using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attack an Enemy and Heal the damage dealt

public class VampireBat : Ability {	

	//FeastOfBlood feastOfBlood;
	bool deadStatus;

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack (attacker, defender);
			
				
		int damageHeal = GameManager.Instance.atk_damage;
		Debug.Log("Cadiz Vampiric Bat damage: " +damageHeal);

		GameManager.Instance.Heal(attacker, damageHeal);

		FeastOfBloodAbility(attacker, defender);
		base.UseAbility(attacker, defender);		


	}


	public void FeastOfBloodAbility(HeroManager attacker, HeroManager defender){

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
			GameManager.Instance.ExtraTurn();
		} 
		defender.gameObject.SetActive(deadStatus);
		
		}	

	}
	
	
