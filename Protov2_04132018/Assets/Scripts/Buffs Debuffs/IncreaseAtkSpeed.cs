using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAtkSpeed : BuffComponent {

	public float value = 25;

			//store the nominal "bonus" value for each buff
	public float bonus;

	void Awake () {
		this.tag = "Buff";
		this.buffName = "IncreaseAtkSpeed";
		this.buffIcon = Resources.Load<Sprite>("BuffIcons/Status_Increase_Speed1");

		bonus = (value/100)*gameObject.GetComponent<Hero>().currAttribs.baseSpeed;

		gameObject.GetComponent<Hero>().currAttribs.baseSpeed += bonus;

	}

	protected override void OnDestroy ()
	{
		gameObject.GetComponent<Hero>().currAttribs.baseSpeed -= bonus;
		base.OnDestroy();
	}

}
