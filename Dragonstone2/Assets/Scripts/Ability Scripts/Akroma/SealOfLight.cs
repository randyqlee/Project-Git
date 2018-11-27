using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealOfLight : Ability {

	public override void UseAbility ()
	{
		Debug.Log ("Using SealOfLight");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using SealOfLight: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

		GameManager.Instance.Attack (attacker, defender);

	}
}
