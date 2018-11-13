using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : BuffComponent {

	EventManager e;
	// Use this for initialization
	void Awake () {
		this.tag = "Buff";
		this.buffName = "Counter";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Counterattack1");

		e = GetComponent<EventManager>();
		e.e_TakeDamage += CAttack;
		gameObject.GetComponent<Hero>().hasCounterAttack = true;
	}

	protected override void OnDestroy () {
		gameObject.GetComponent<Hero>().hasCounterAttack = false;
		base.OnDestroy();
	}


	void CAttack(GameObject source, GameObject target)
	{
		//swapped target <-> source since this is counterattack
		target.GetComponent<DamageSystem>().BasicAttack(target, source, 1, 0);
		
		Debug.Log ("Counter Attack");

	}
}
