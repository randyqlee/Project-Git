using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weaken : Ability {

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{		
		GameManager.Instance.Attack(attacker, defender);

		Button[] button = attacker.GetComponentsInChildren<Button>();
		Ability ability = button[2].GetComponent<Ability>();

		ability.UseAbility(attacker, defender);

		base.UseAbility(attacker, defender);		

	}
	
}
