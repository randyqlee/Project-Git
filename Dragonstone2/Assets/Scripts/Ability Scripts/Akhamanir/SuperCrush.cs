using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCrush : Ability {


	public override void UseAbility ()
	{
		Debug.Log ("Using  SuperCrush");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{

			Debug.Log ("Using  SuperCrush");

			GameManager.Instance.AttackAll (attacker, defender);
	

	}
}
