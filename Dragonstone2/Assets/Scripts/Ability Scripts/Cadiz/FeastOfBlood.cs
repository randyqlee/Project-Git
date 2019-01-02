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
		FeastOfBloodAbility(attacker, defender);
	}

	public void FeastOfBloodAbility(HeroManager attacker, HeroManager defender){

		//Add Brand
		
		if(!(defender.hasImmunity || defender.hasPermanentImmunity)){

		}else{
			GameManager.Instance.AddDebuff("Brand", 2, attacker, defender);	
		}
		
		
		
		//Heal all for 30
		int healValue = 30;
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(attacker);
		foreach(HeroManager ally in allies){
			GameManager.Instance.Heal(ally, healValue);
		}

		//Extra Turn
		if(defender.isDead){
			GameManager.Instance.ExtraTurn();
		} 


	}

	

}
