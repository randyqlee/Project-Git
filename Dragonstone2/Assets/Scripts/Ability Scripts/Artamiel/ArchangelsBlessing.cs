using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchangelsBlessing : Ability {

	//Archangel's Blessing: Heal an ally for 400 Hp with chance to gain Recovery for 2 turns.	
	
	public int healValue = 400;		
	
	public override void UseAbility (HeroManager attacker, HeroManager defender)	
	{
		GameManager.Instance.Heal(defender, healValue);

		base.UseAbility(attacker, defender);

	}
}
