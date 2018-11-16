using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAttack : BuffComponent {

	public float value = -50;

			//store the nominal "bonus" value for each buff
	public float bonus;

	void Awake () {
		this.tag = "Debuff";
		this.buffName = "DecreaseAttack";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Decrease_Attack_Power");

		bonus = (value/100)*gameObject.GetComponent<Hero>().currAttribs.baseDamage;

		gameObject.GetComponent<Hero>().currAttribs.baseDamage += bonus;
	}

		protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().currAttribs.baseDamage -= bonus;
		base.OnDestroy();
	}
}
