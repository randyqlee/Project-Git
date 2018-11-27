using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  GodsShield : Ability {

	public override void UseAbility ()
	{
		Debug.Log ("Using   GodsShield");

	}

	public override void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using  GodsShield: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

		GameManager.Instance.Attack (attacker, defender);

	}
}
