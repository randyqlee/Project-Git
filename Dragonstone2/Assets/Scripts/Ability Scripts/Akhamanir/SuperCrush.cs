using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCrush : Ability {


	int targetCount = 2;
	public override void UseAbility ()
	{

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{


			Debug.Log ("Using  SuperCrush");

			GameManager.Instance.AttackAll (attacker, defender);

			base.UseAbilityRandom (attacker,defender,targetCount);
	

	}
}
