using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sweet Dreams: Attack an enemy and remove all buffs with a chance to stun 1 for turn. 


public class SweetDreams : Ability 
{
	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		GameManager.Instance.Attack(attacker, defender);

		GameManager.Instance.DestroyAllBuffs(defender);

		base.UseAbility(attacker, defender);

	}

}
