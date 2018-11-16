using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamage : BuffComponent {

	public float value = 5;

	EventManager e;

	void Awake () {


		this.tag = "Debuff";
		this.buffName = "ContinuousDamage";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/ContinuousDamage");
		e = GetComponent<EventManager>();
		e.e_ResetATB += TakeDamage;


	}

	public void TakeDamage ()
	{
		float damage = gameObject.GetComponent<Hero>().attributes.baseHP * (1 - value/100);
		gameObject.GetComponent<HealthSystem>().TakeDamage(damage);

		//TakeDamage per turn. DO NOT USE MODIFY SCRIPTS.

	}

}
