using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSmash : Ability {

	public override void UseAbility ()
	{
		Debug.Log ("Using  MegaSmash");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using Megasmash: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

		GameManager.Instance.Attack (attacker, defender);

	}
}
