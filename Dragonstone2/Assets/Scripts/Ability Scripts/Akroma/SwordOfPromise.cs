using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOfPromise : Ability {

	public override void UseAbility ()
	{
		Debug.Log ("Using  SwordOfPromise");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using SwordOfPromise: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

		GameManager.Instance.Attack (attacker, defender);

	}
}
