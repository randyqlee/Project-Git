using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

	public int abilityCooldown;

	public virtual void UseAbility ()
	{

	}

	public virtual void UseAbility (HeroManager attacker, HeroManager defender)
	{
		Debug.Log ("Using ability: attacker - " + attacker.gameObject.name + " , defender - " + defender.gameObject.name);

	}
}
