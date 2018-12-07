using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSmash : Ability {

	int buffDuration = 2;

	public override void UseAbility ()
	{
		Debug.Log ("Using  MegaSmash");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using Megasmash: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);



		//just for testing of buff
		//defender.gameObject.AddComponent<Poison>().New(buffDuration,gameObject);

		GameManager.Instance.AddDebuffComponent<Poison>(buffDuration,attacker,defender);

		GameManager.Instance.Attack (attacker, defender);


	}
}
