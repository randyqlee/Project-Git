using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFan : Ability {

	//Attack and deal critical strike to an enemy 
	//Chance to remove up to 3 buffs (random).

	Buff[] buffs;
	
	public override void UseAbility(HeroManager attacker, HeroManager defender){

		DestroyThreeBuffs(defender);
		defender.UpdateUI();
		GameManager.Instance.OneTurnCritical(attacker, defender);		

		base.UseAbility(attacker, defender);

	}

	//OnDestroy, check Critical buff first before setting hasCritical = false;
	void DestroyThreeBuffs(HeroManager target){
		buffs = target.GetComponents<Buff>();

		if(buffs.Length <=3){
			foreach(Buff buff in buffs){
					
				buff.OnDestroy();
					
			}
			

		} //if buffs less than or equal to 3
		else {
			for(int i =0; i<3; i++){
				//need to randomize
				buffs[i].OnDestroy();
						
			}
			
		}//if buffs is greater than 3
	}

}
