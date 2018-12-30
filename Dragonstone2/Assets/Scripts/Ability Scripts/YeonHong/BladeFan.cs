using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeFan : Ability {

	//Attack and deal critical strike to an enemy 
	//Chance to remove up to 3 buffs (random).

	Buff[] buffs;
	//List<Buff> buffs = new List<Buff>();

	
	public override void UseAbility(HeroManager attacker, HeroManager defender){

		DestroyThreeBuffs(defender);
		defender.UpdateUI();
		//GameManager.Instance.OneTurnCritical(attacker, defender);		

		bool criticalStatus = attacker.hasCritical;
		attacker.hasCritical = true;
		GameManager.Instance.Attack(attacker, defender);
		attacker.hasCritical = criticalStatus;
		

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
			Shuffle(buffs);
			for(int i =0; i<3; i++){
				//need to randomize
				buffs[i].OnDestroy();
						
			}
			
		}//if buffs is greater than 3
	}//Destroy three buffs

	//Fisher-Yates shuffle
	void Shuffle(Buff[] buffsList){
		int n = buffsList.Length;
		while(n>0){
			n--;
			int k = Random.Range(0,n);
			Buff valueTemp = buffsList[k];
			buffsList[k] = buffsList[n];
			buffsList[n] = valueTemp;

		} 

	}//shuffle

}
