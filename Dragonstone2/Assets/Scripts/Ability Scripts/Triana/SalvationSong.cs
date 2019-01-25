using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Salvation Song : Offsets the incoming damage that may cause your ally to die and instantly gains another turn. 
//Automatic CD: 5 turns


public class SalvationSong : Ability {

	public override void UseAbilityPassive(){

		GameManager.Instance.e_DealDamage += SalvationSongAbility;
		Debug.Log("Ability Subscribed: " +this.GetComponent<Ability>().name);

	}

	public override void DisableAbilityPassive(){

		GameManager.Instance.e_DealDamage -= SalvationSongAbility;

	}

	public override void UseAbility(HeroManager attacker, HeroManager defender){

		//SalvationSongAbility(damage, target);
	}
	
	
	public void SalvationSongAbility (int damage, HeroManager target)
	{
		Debug.Log("Salvation Song Ability");
			if(remainingCooldown == 0){			
				target.maxHealth += damage;
				target.UpdateUI();
				ResetCooldown();
			

			
		}

	}

}//class
