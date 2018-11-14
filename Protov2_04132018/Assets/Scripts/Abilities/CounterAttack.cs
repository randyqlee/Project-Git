using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Abilities/CounterAttack")]
public class CounterAttack : Ability {



	void OnEnable ()
	{
		EventManager e = parent.GetComponent<EventManager>();
		Debug.Log ("Event manager setup for: " + e.gameObject.name);
		//subscribe to TakeDamage events
		e.e_TakeDamage += CAttack;
	}

	void OnDisable ()
	{
		EventManager e = GameObject.FindObjectOfType<EventManager>();
		e.e_TakeDamage -= CAttack;
	}

	void CAttack(GameObject target, GameObject attacker)
	{
		attacker.GetComponent<HealthSystem>().TakeDamage(target.GetComponent<Character>().hero.attributes.baseDamage);
		Debug.Log ("Counter Attack");
	}


}
