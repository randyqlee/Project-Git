using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lightning Strike: Attack an enemy with a chance to steal a random buff.

public class LightningStrike : Ability {	

public override void UseAbility (HeroManager attacker, HeroManager defender)
	{	
		GameManager.Instance.Attack(attacker, defender);

		if(GameManager.Instance.IsChanceSuccess(attacker)){
			BuffSteal(attacker, defender);
		}

		base.UseAbility(attacker, defender);		

	}


	void BuffSteal(HeroManager attacker, HeroManager defender){

		Buff[] buffs = defender.GetComponents<Buff>();

		if(buffs.Length > 0){
			//Debug.Log("Debuffs Count: " +buffs.Length);

		//Get Random Buff		
		int count = buffs.Length;
		Buff transferBuff = buffs[Random.Range(0,count)];

		//Get details
		string transferBuffName = transferBuff.buff.buff.ToString();
		//Debug.Log("Buff to be transferred: " +transferBuffName);
		int buffDuration = transferBuff.duration;

		//Transfer Buff
		GameManager.Instance.AddBuff(transferBuffName, buffDuration, defender, attacker);						

		transferBuff.OnDestroy();	

		} else {
			Debug.Log("No Buffs to transfer");
		}
	}


	

}
