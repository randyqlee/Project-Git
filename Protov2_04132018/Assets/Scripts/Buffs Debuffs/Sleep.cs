using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : BuffComponent {

	EventManager e;

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "Sleep";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Sleep");		
		gameObject.GetComponent<Hero>().isSleep = true;

		e = GetComponent<EventManager>();
		e.e_TakeDamage += DamageTaken;
	}


	void DamageTaken (GameObject target, GameObject source)
	{
		e.popupMsg("Awaken from Sleep");
		OnDestroy();
	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().isSleep = false;
		base.OnDestroy();
	}
}
