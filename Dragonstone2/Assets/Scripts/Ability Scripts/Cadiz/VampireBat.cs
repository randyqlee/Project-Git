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

		//Access Feast of Blood
		Button[] buttons = attacker.GetComponentsInChildren<Button>();
		Ability feastOfBlood = buttons[2].GetComponent("FeastOfBlood") as Ability;
		feastOfBlood.UseAbility(attacker, defender);

		//bility feastofBlood2 = attacker.GetComponentInChildren("FeastOfBlood") as Ability;

		base.UseAbility(attacker, defender);		


	}	

}
	
	
