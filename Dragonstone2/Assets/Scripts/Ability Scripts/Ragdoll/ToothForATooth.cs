using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tooth For a Tooth:  An ally gains Lucky for 2 turns when hit by a critical attack. [Automatic Effect]

public class ToothForATooth : Ability {

HeroManager hero;

	public override void UseAbilityPassive()
		{
			GameManager.Instance.e_CriticalStrike += TooThForToothAbility;
			Debug.Log("Ability Subscribed");
			base.UseAbilityPassive();
		}

		public override void DisableAbilityPassive()
		{
			GameManager.Instance.e_CriticalStrike -= TooThForToothAbility;
			base.UseAbilityPassive();
		}

	void TooThForToothAbility()
	{
		hero = GetComponentInParent<HeroManager>();
		List<HeroManager> allies = GameManager.Instance.AllyHeroList(hero);

		foreach(HeroManager ally in allies){

			if(ally.hitByCritical){
				
				if(ally.GetComponentInParent<Player>().isActive){
					GameManager.Instance.AddBuff("Lucky", 2, hero, ally);
					//ally.hitByCritical = false;
				}else{
					GameManager.Instance.AddBuff("Lucky", 3, hero, ally);
					//ally.hitByCritical = false;
				}								
			}//if ally hitByCritical
		}//foreach
	}//ToothForTooth
}
