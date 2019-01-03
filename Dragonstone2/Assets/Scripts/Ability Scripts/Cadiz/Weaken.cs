using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weaken : Ability {
// 
	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{		
		GameManager.Instance.Attack(attacker, defender);

		//Access Feast of Blood
		Button[] buttons = attacker.GetComponentsInChildren<Button>();
		Ability feastOfBlood = buttons[2].GetComponent<Ability>();
		feastOfBlood.UseAbility(attacker, defender);		

		base.UseAbility(attacker, defender);		

	}
	
}
