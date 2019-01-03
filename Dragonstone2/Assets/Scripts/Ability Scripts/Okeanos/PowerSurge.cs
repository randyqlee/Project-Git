using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSurge : Ability {

//Attack an enemy.  Transfer a random debuff from you to the enemy.

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack (attacker, defender);		

		DebuffTransfer(attacker, defender);	

		
		base.UseAbility();		

	}

	void DebuffTransfer(HeroManager attacker, HeroManager defender){

		Debuff[] debuffs = attacker.GetComponents<Debuff>();

		if(debuffs.Length > 0){
			Debug.Log("Debuffs Count: " +debuffs.Length);

		//Get Random Debuff
		
		int count = debuffs.Length;
		Debuff transferDebuff = debuffs[Random.Range(0,count)];

		//Get details
		string transferDebuffName = transferDebuff.debuff.debuff.ToString();
		Debug.Log("Debuff to be transferred: " +transferDebuffName);
		int debuffDuration = transferDebuff.duration;

		//Transfer Debuff
		if(defender.hasImmunity || defender.hasPermanentImmunity){
			
			Debug.Log ("Target Hero has immunity");

		}else{

			GameManager.Instance.AddDebuff(transferDebuffName, debuffDuration, attacker, defender);
			// //Destroy Debuff
			
			
		}

		transferDebuff.OnDestroy();
		
		


		} else {
			Debug.Log("No Debuffs to transfer");
		}
		
		

		



	}

}//Ability
