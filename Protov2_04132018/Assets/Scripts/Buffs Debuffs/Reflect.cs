using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : BuffComponent {

	EventManager e;
	public float value = 33;


	void Awake () {
		this.tag = "Buff";
		this.buffName = "Reflect";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Reflect_Damage1");

		gameObject.GetComponent<Hero>().hasReflect = true;

		e = GetComponent<EventManager>();
		e.e_TakeDamage += ReflectDamage;


		
	}

	void ReflectDamage (GameObject target, GameObject source)
	{

		
		//what damage will be reflected

	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().hasReflect = false;
		base.OnDestroy();
	}
}